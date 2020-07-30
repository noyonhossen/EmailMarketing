using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Transactions;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models;
using EmailMarketing.Web.Models.Account;
using EmailMarketing.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace EmailMarketing.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMailerService _mailerService;
        private readonly IWebHostEnvironment _env;

        public AccountController(ApplicationSignInManager signInManager,
            ILogger<AccountController> logger,
            ApplicationUserManager userManager,
            IEmailSender emailSender,
            IMailerService mailerService,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _mailerService = mailerService;
            _env = env;
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ExternalLogins"] = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                var user = await _userManager.FindByEmailAsync(model.Email);
                var isValidUser = await _userManager.CheckPasswordAsync(user, model.Password);

                var roles = await _userManager.GetRolesAsync(user);

                if (isValidUser)
                {
                    if(!(await _userManager.IsEmailConfirmedAsync(user)))
                    {
                        ModelState.AddModelError(string.Empty, "Please confirm your email account.");
                    }
                    else if(user.PasswordChangedCount == 0)
                    {
                        return RedirectToAction("ChangeDefaultPassword", new { email = user.Email });
                    }
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        }
                        else if (roles.Any(x => x.Equals(ConstantsValue.UserRoleName.Member)))
                        {
                            _logger.LogInformation("User logged in.");
                            return RedirectToAction(nameof(Index), "Dashboard", new { Area = "Member" });
                        }
                        else if (roles.Any(x => x.Equals(ConstantsValue.UserRoleName.SuperAdmin) || x.Equals(ConstantsValue.UserRoleName.Admin)))
                        {
                            _logger.LogInformation("Admin logged in.");
                            return RedirectToAction(nameof(Index), "AdminUsers", new { Area = "Admin" });
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If we got this far, something failed, redisplay form
            //return Page();
            return View(model);
        }

        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            ViewData["ExternalLogins"] = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            ViewData["ExternalLogins"] = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName, PasswordChangedCount = 1 };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            await _userManager.AddToRoleAsync(user, ConstantsValue.UserRoleName.Member);
                            _logger.LogInformation("Role --Member-- assined to User");

                            //Email Verification Section
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                            //Activate Email Link
                            var emailVerificationlink = Url.Action(nameof(VerifyEmail), "Account", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());

                            //String Body
                            var webroot = _env.WebRootPath;

                            var pathToFile = _env.WebRootPath
                                    + Path.DirectorySeparatorChar.ToString()
                                    + "Templates"
                                    + Path.DirectorySeparatorChar.ToString()
                                    + "EmailTemplate"
                                    + Path.DirectorySeparatorChar.ToString()
                                    + "Confirm_Account_Registration.html";

                            var subject = "Confirm Account Registration";

                            var builder = new BodyBuilder();
                            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                            {
                                builder.HtmlBody = SourceReader.ReadToEnd();
                            }

                            string messageBody = string.Format(builder.HtmlBody,
                                    user.FullName,
                                    emailVerificationlink
                                    );

                            await _mailerService.SendEmailAsync(user.Email, subject, messageBody);

                            scope.Complete();

                            return RedirectToAction("EmailVerificationConfirmation");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        ModelState.AddModelError(string.Empty, "Something went wrong. Please try again.");
                    }
                }

            }

            // If we got this far, something failed, redisplay form
            //return Page();

            return View(model);
        }

        public async Task<IActionResult> AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            ViewData["ExternalLogins"] = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action(nameof(ResetPassword), "Account", new { email = model.Email, code }, 
                                                    Request.Scheme, Request.Host.ToString());

                //String Body
                var webroot = _env.WebRootPath;

                var pathToFile = _env.WebRootPath
                        + Path.DirectorySeparatorChar.ToString()
                        + "Templates"
                        + Path.DirectorySeparatorChar.ToString()
                        + "EmailTemplate"
                        + Path.DirectorySeparatorChar.ToString()
                        + "Reset_Account_Password.html";

                var subject = "Recover Account Password";

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }

                //Inserting Value to Html body dynamically
                string messageBody = string.Format(builder.HtmlBody,
                        user.FullName,
                        passwordResetLink
                        );

                await _mailerService.SendEmailAsync(user.Email, subject, messageBody);

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string code)
        {
            if (email == null || code == null)
            {
                ModelState.AddModelError("", "Invalid Password Reset Token");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                user.PasswordChangedCount += 1;
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if(result.Succeeded)
                {
                    var updatedResult = await _userManager.UpdateAsync(user);
                    if(updatedResult.Succeeded)
                    {
                        return RedirectToAction("ResetPasswordConfirmation");
                    }
                    //ModelState.AddModelError(String.Empty, "Something went wrong. Please Try again");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult EmailVerificationConfirmation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Login", new { returnUrl });
            }

        }   


        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                var updatedResult = await _userManager.UpdateAsync(user);

                if (updatedResult.Succeeded)
                {
                    return View();
                }

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult ChangeDefaultPassword(string email)
        {
            var model = new ChangeDefaultPasswordViewModel();
            model.Email = email;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeDefaultPassword(ChangeDefaultPasswordViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

                if(result.Succeeded)
                {
                    user.PasswordChangedCount = 1;
                    var updatedResult = await _userManager.UpdateAsync(user);

                    if(updatedResult.Succeeded)
                    {
                        return RedirectToAction("ChangePasswordConfirmation");
                    }
                }
            }
            return View(model);
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }


    }
}

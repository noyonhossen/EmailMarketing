using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Controllers
{
    //  [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger<ProfileController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(ApplicationSignInManager signInManager,
           ILogger<ProfileController> logger,
           ApplicationUserManager userManager,
           IEmailSender emailSender,
           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(
            [Bind(nameof(ChangePasswordModel.CurrentPassword),
            nameof(ChangePasswordModel.NewPassword),
            nameof(ChangePasswordModel.ConfirmNewPassword))] ChangePasswordModel model)

        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                try
                {
                    await model.ChangePasswordAsync(user);
                    _logger.LogInformation("Successfully Changed Password");
                }
                catch
                {
                    _logger.LogInformation("Failed to Change Password");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var model = Startup.AutofacContainer.Resolve<ProfileInformationModel>();
            model.Load();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateInformation()
        {
            var model = new UpdateInformationModel();
            await model.Load();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInformation(
            [Bind(nameof(UpdateInformationModel.FullName),
            nameof(UpdateInformationModel.PhoneNumber),
            nameof(UpdateInformationModel.DateOfBirth),
            nameof(UpdateInformationModel.Address))] UpdateInformationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    _logger.LogInformation("Successfully Changed Information");
                    return RedirectToAction("Profile");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Failed to Update Profile Infomation");
                }
            }
            return View(model);
        }
    }
}
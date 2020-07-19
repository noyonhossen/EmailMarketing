using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger<ProfileController> _logger;
        private readonly IEmailSender _emailSender;

        public ProfileController(ApplicationSignInManager signInManager,
           ILogger<ProfileController> logger,
           ApplicationUserManager userManager,
           IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)

        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user==null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if(result.Succeeded)
                _logger.LogInformation("Successfully Changed Password");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(ProfileInformationModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.FullName = user.FullName;
            model.DateOfBirth = user.DateOfBirth;
            model.Address = user.Address;
            //model.Id = user.Id;
            model.Email = user.Email;
            return View(model);
        }

        //[HttpPost]
        //public 
        //{

        //}


        [HttpGet]
        public IActionResult UpdateInformation()
        {
            //var model = new ProfileInformationModel();
            //model.Load(id);
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInformation(ProfileInformationModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.FullName = model.FullName;
                user.DateOfBirth = model.DateOfBirth;
                user.Address = model.Address;
                var result = await _userManager.UpdateAsync(user);
                //var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Successfully Changed Information");
                    return RedirectToAction("Profile");
                }
            }

            return View(model);

        }
    }
}
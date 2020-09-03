using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Common.Services;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.ProfileModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger<ProfileController> _logger;
        private readonly IEmailSender _emailSender;

        private readonly IApplicationUserService _applicationUserService;
        private readonly ICurrentUserService _currentUserService;
        public ProfileController(ApplicationSignInManager signInManager,
           ILogger<ProfileController> logger,
           ApplicationUserManager userManager,
           IEmailSender emailSender,
           IApplicationUserService applicationUserService,
           ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _applicationUserService = applicationUserService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProfileInformationModel model)
        {
            await model.LoadInfo();
            return View(model);
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel(); 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await model.ChangeMemberPasswordAsync();
                    if (result == true)
                    {
                        _logger.LogInformation("Successfully Changed Password.");
                        TempData["SuccessNotify"] = "Successfully Changed Password.";
                        return RedirectToAction("Index","Dashboard");
                    }
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel(ex.Message,ResponseType.Failure);
                    _logger.LogInformation("Failed to Change Password.");
                }
            }
            return View(model);
        }
         
        [HttpGet]
        public async Task<IActionResult> ChangePasswordConfirmation(ChangePasswordConfirmationModel model)
        {
            await _signInManager.SignOutAsync();
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
            nameof(UpdateInformationModel.Email),
            nameof(UpdateInformationModel.PhoneNumber),
            nameof(UpdateInformationModel.DateOfBirth),
            nameof(UpdateInformationModel.Gender),
            nameof(UpdateInformationModel.Address))] UpdateInformationModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateMemberAsync();
                    _logger.LogInformation("Member Information Updated Successfully");
                    TempData["SuccessNotify"] = "Successfully Updated Information.";
                    return RedirectToAction("Index");
                }
                catch
                {
                    _logger.LogInformation("Failed to Update Memeber Information"); 
                    model.Response = new ResponseModel("Failed to Update", ResponseType.Failure);
                }
            }
            return View(model);
        }
    }
}
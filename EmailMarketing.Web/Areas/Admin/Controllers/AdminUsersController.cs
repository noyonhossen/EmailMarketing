using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Controllers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        public AdminUsersController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ILoggerFactory logger,
            IEmailSender emailSender
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger.CreateLogger<AdminUsersController>();
            _emailSender = emailSender;
           
        }

        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<AdminUsersModel>();
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new CreateAdminUsersModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ImageUrl = model.ImageUrl

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditAdminUsersModel();
            var user = await _userManager.FindByIdAsync(id.ToString());
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ImageUrl = model.ImageUrl

                };

                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                    return RedirectToAction("Edit");

            }
            return View(model);

          
        }
        public async Task<IActionResult> GetAdminUsers()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<AdminUsersModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }
    }
}
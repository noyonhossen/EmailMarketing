using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Data.Exceptions;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Enums;
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
        public async Task<IActionResult> Add()
        {
            var model = new CreateAdminUsersModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [Bind(nameof(CreateAdminUsersModel.FullName),
            nameof(CreateAdminUsersModel.UserName),
            nameof(CreateAdminUsersModel.Password),
            nameof(CreateAdminUsersModel.Email),
            nameof(CreateAdminUsersModel.PhoneNumber))]CreateAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateAdmin();
                    model.Response = new ResponseModel("Record Added successful.", ResponseType.Success);

                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Record added failed.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var model = new EditAdminUsersModel();
            await model.LoadByIdAsync(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(nameof(EditAdminUsersModel.Id),
            nameof(EditAdminUsersModel.FullName),
            nameof(EditAdminUsersModel.UserName),
            nameof(EditAdminUsersModel.Email),
            nameof(EditAdminUsersModel.PhoneNumber))]EditAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("Record Updated successful.", ResponseType.Success);

                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    // error logger code
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Record Update failed.", ResponseType.Failure);
                    // error logger code
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminUsersModel();
                try
                {
                    await model.DeleteAdminAsync(id);
                    model.Response = new ResponseModel($"Admin successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin delete failued.", ResponseType.Failure);
                    // error logger code
                }
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> ShowProfile(AdminUsersShowProfileModel model)
        {
             await model.ShowProfileAsync();
            //return View(model);
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
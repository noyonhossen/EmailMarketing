using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Enums;
using EmailMarketing.Web.Areas.Admin.Models;
using EmailMarketing.Web.Areas.Admin.Models.AdminUsers;
using EmailMarketing.Web.Controllers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ILogger<AdminUsersController> _logger;

        public AdminUsersController(ILogger<AdminUsersController> logger)
        {
            _logger = logger;
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
        public async Task<IActionResult> Add(
            [Bind(nameof(CreateAdminUsersModel.FullName),
            nameof(CreateAdminUsersModel.Email),
            nameof(EditAdminUsersModel.DateOfBirth),
            nameof(EditAdminUsersModel.PhoneNumber),
            nameof(EditAdminUsersModel.Gender),
            nameof(EditAdminUsersModel.Address))] CreateAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateAdminAsync();
                    model.Response = new ResponseModel("Admin Added successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin added failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditAdminUsersModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(nameof(EditAdminUsersModel.Id),
            nameof(EditAdminUsersModel.FullName),
            nameof(EditAdminUsersModel.Email),
            nameof(EditAdminUsersModel.DateOfBirth),
            nameof(EditAdminUsersModel.PhoneNumber),
            nameof(EditAdminUsersModel.Gender),
            nameof(EditAdminUsersModel.Address))] EditAdminUsersModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("Admin Updated successful.", ResponseType.Success);

                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin Update failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
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
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Admin {title}  successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin delete failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> ShowProfile(AdminUsersShowProfileModel model)
        {
            await model.ShowProfileAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateInformation()
        {
            var model = new AdminUsersProfileUpdateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInformation(
            [Bind(nameof(AdminUsersProfileUpdateModel.FullName),
            nameof(AdminUsersProfileUpdateModel.Email),
            nameof(AdminUsersProfileUpdateModel.DateOfBirth),
            nameof(AdminUsersProfileUpdateModel.PhoneNumber),
            nameof(AdminUsersProfileUpdateModel.Gender),
            nameof(AdminUsersProfileUpdateModel.Address))] AdminUsersProfileUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateProfileAsync();
                    model.Response = new ResponseModel($"Admin successfully updated.", ResponseType.Success);
                    return RedirectToAction("ShowProfile");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Admin update failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            if (ModelState.IsValid)
            {
                var model = new AdminUsersModel();
                try
                {
                    var user = await model.BlockUnblockAsync(id);
                    model.Response = new ResponseModel($"Admin {user.Name} successfully { (user.IsBlocked == true ? "Blocked" : "Unblocked")}.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Block/Unblock Operation failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(Guid id)
        {
            if (ModelState.IsValid)
            {
                var model = new  AdminUsersModel();
                try
                {
                    var title = await model.ResetPasswordAsync(id);
                    model.Response = new ResponseModel($"Admin {title} Password Reset Successfully.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Password Reset failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetUsers()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<AdminUsersModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }
        public async Task<IActionResult> UserInformation(Guid id)
        {
            var model = new AdminInformationModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }
    }
}
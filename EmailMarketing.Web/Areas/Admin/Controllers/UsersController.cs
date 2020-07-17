using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Data.Exceptions;
using EmailMarketing.Membership.Constants;
using EmailMarketing.Membership.Entities;
using EmailMarketing.Membership.Enums;
using EmailMarketing.Membership.Services;
using EmailMarketing.Web.Areas.Admin.Enums;
using EmailMarketing.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmailMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        

        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<UserModel>();
            return View(model);
        }

        public async Task<IActionResult> GetUsers()
        {
            
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<UserModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditUserModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }
        public async Task<IActionResult> UserInformation(Guid id)
        {
            var model = new UserInformationModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(nameof(EditUserModel.Id),
            nameof(EditUserModel.UserName),
            nameof(EditUserModel.Email),
            nameof(EditUserModel.Gender),
            nameof(EditUserModel.Address),
            nameof(EditUserModel.FullName),
            nameof(EditUserModel.PhoneNumber),
            nameof(EditUserModel.ImageUrl)
            )] EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("User edit successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("User edit failured.", ResponseType.Failure);
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
                var model = new UserModel();
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"User {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("User delete failured.", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUser(Guid id)
        {
            if (ModelState.IsValid)
            {
                var model = new UserModel();
                try
                {
                    var title = await model.BlockUser(id);
                    model.Response = new ResponseModel($"User {title} successfully blocked.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("User block failured.", ResponseType.Failure);
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
                var model = new UserModel();
                try
                {
                    var title = await model.UpdatePasswordHash(id);
                    model.Response = new ResponseModel($"User {title} Password Reset Successfully.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Password Reset failured.", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
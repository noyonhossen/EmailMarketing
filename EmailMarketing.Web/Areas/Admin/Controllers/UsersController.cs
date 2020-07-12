﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var model = Startup.AutofacContainer.Resolve<UsersModel>();
            return View();
        }

        public IActionResult Add()
        {
            var model = new CreateUsersModel();
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(
        //    [Bind(nameof(CreateExpenseModel.Title),
        //    nameof(CreateExpenseModel.Description),
        //    nameof(CreateExpenseModel.Amount),
        //    nameof(CreateExpenseModel.ExpenseType),
        //    nameof(CreateExpenseModel.ExpenseDate))] CreateExpenseModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await model.AddAsync();
        //            model.Response = new ResponseModel("Expense creation successful.", ResponseType.Success);
        //            return RedirectToAction("Index");
        //        }
        //        catch (DuplicationException ex)
        //        {
        //            model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
        //        }
        //        catch (Exception ex)
        //        {
        //            model.Response = new ResponseModel("Expense creation failured.", ResponseType.Failure);
        //        }
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var model = new EditExpenseModel();
        //    await model.LoadByIdAsync(id);
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(
        //    [Bind(nameof(EditExpenseModel.Id),
        //    nameof(CreateExpenseModel.Title),
        //    nameof(CreateExpenseModel.Description),
        //    nameof(CreateExpenseModel.Amount),
        //    nameof(CreateExpenseModel.ExpenseType),
        //    nameof(CreateExpenseModel.ExpenseDate))] EditExpenseModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await model.UpdateAsync();
        //            model.Response = new ResponseModel("Expense edit successful.", ResponseType.Success);
        //            return RedirectToAction("Index");
        //        }
        //        catch (DuplicationException ex)
        //        {
        //            model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
        //        }
        //        catch (Exception ex)
        //        {
        //            model.Response = new ResponseModel("Expense edit failured.", ResponseType.Failure);
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var model = new ExpenseModel();
        //        try
        //        {
        //            var title = await model.DeleteAsync(id);
        //            model.Response = new ResponseModel($"Expense {title} successfully deleted.", ResponseType.Success);
        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception ex)
        //        {
        //            model.Response = new ResponseModel("Expense delete failured.", ResponseType.Failure);
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> GetUsers()
        //{
        //    var tableModel = new DataTablesAjaxRequestModel(Request);
        //    var model = Startup.AutofacContainer.Resolve<UsersModel>();
        //    //var data = await model.GetUsers(tableModel);
        //    return Json(data);
        //}
    }
}
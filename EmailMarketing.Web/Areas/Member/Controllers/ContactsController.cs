using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = Startup.AutofacContainer.Resolve<ContactsModel>();
            return View(model);
        }

        public IActionResult ManageUploads()
        {
            var model = new ContactsModel();
            return View(model);
        }

        public IActionResult CustomFields()
        {
            var model = Startup.AutofacContainer.Resolve<FieldMapModel>();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            var model = new FieldMapModel();

            #region for edit
            if (id.HasValue && id != 0)
            {
                await model.LoadByIdAsync(id.Value);
            }
            #endregion

            return PartialView("_AddOrEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(FieldMapModel model)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                if (model.Id.HasValue && model.Id != 0) await model.UpdateFieldMapAsync();
                else await model.AddFieldMapAsync();

                TempData["SuccessNotify"] = "Field has been successfully saved";
                return RedirectToAction("CustomFields");
            }

            TempData["ErrorNotify"] = "Field could not be saved";
            return RedirectToAction("CustomFields");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFieldMap(int id)
        {
            var model = new FieldMapModel();
            await model.DeleteFieldMapAsync(id);
            return Json(true);
        }
        public async Task<IActionResult> GetAllFieldMap()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<FieldMapModel>();
            var data = await model.GetAllFieldMapAsync(tableModel);
            return Json(data);
        }

        public IActionResult AddSingleContact()
        {
            var model = new ContactsModel();
            return View(model);
        }

        public async Task<IActionResult> EditContact(int id)
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact()
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var model = new ContactsModel();
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Contact {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Contact delete failured.", ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetContacts()
        {

            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<ContactsModel>();
            var data = await model.GetAllContactAsync(tableModel);
            return Json(data);
        }
        public async Task<IActionResult> ContactDetails(int id)
        {
            var model = new ContactDetailsModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }
    }
}
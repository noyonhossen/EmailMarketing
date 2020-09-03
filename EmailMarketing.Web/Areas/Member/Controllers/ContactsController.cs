using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> ActivateFieldMap(int id)
        {
            var model = new FieldMapModel();
            try
            {
                var customFieldMap = await model.ActivateFieldMapAsync(id);
                model.Response = new ResponseModel($"{customFieldMap.DisplayName} successfully { (customFieldMap.IsActive == true ? "Activated" : "Deactivated")}.", ResponseType.Success);
                _logger.LogInformation("Custome Field Map Active Status updated");
            }
            catch (Exception ex)
            {
                model.Response = new ResponseModel("Active/InActive Operation failured.", ResponseType.Failure);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("CustomFields");
        }
        public async Task<IActionResult> GetAllFieldMap()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<FieldMapModel>();
            var data = await model.GetAllFieldMapAsync(tableModel);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddSingleContact()
        {
            var model = new AddSingleContactModel();
            await model.LoadContactInformationAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSingleContact(AddSingleContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingContact = await model.IsContactExistAsync();
                    if (existingContact == true)
                    {
                        model.Response = new ResponseModel("Contact already exist. You can update the existing contact.", ResponseType.Failure);                
                    }
                    else
                    {
                        await model.SaveContactAsync();
                        var msg = "Added Contact Successfully";
                        _logger.LogInformation("Single Contact Added Successfully");
                        model.Response = new ResponseModel(msg, ResponseType.Success);
                    }
                    //return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //var msg = "Failed to Add Contact";
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            await model.LoadContactInformationAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditContact(int id)
        {
            var model = new EditContactsModel();
            await model.LoadContactByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditContact(EditContactsModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    _logger.LogInformation("Contact Successfully Updated.");
                    model.Response = new ResponseModel("Contact Updated",ResponseType.Success);
                    return RedirectToAction("Index");
                }
                 catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
            }
            await model.LoadContactByIdAsync(model.Id);
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

        
        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var model = new ContactExportModel();
            model.GroupSelectList = await model.GetAllGroupDetailsAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Export(
            ContactExportModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.IsExportAll)
                    {
                        await model.ExportAllContact();
                    }
                    else
                    {
                        await model.ExportContactsGroupwise();
                    }
                    _logger.LogInformation("Succecssfully Added to DownloadQueue. Waiting to Complete to Export");
                }
                catch
                {
                    model.Response= new ResponseModel("Please provide Email", ResponseType.Failure);
                }
            }

            model.GroupSelectList = await model.GetAllGroupDetailsAsync();
            return View(model);
        }


    }
}

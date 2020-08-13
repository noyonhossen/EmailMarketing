using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var model = new ContactsModel();
            model.Contacts = await model.GetAllContactAsync();
            return View(model);
        }

        public IActionResult ManageUploads()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult CustomFields()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult EditCustomField()
        {
            var model = new ContactsModel();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddSingleContact()
        {
            var model = new SingleContactModel();
            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            model.ContactValueMaps = await model.GetAllContactValueMaps();
            model.ContactValueMapsCustom = await model.GetAllContactValueMapsCustom();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSingleContact(
            [Bind(nameof(SingleContactModel.Email),
            nameof(SingleContactModel.GroupSelectList),
            nameof(SingleContactModel.ContactValueMaps),
            nameof(SingleContactModel.ContactValueMapsCustom))] SingleContactModel model)
        {
            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            model.ContactValueMaps = await model.GetAllContactValueMaps();
            model.ContactValueMapsCustom = await model.GetAllContactValueMapsCustom();

            if (ModelState.IsValid)
            {
                try
                {
                    await model.SaveContactAsync();
                    var msg = "Congrats! Added Contact Successfully";
                    _logger.LogInformation("Single Contact Added Successfully");
                    model.Response = new ResponseModel(msg, ResponseType.Success);
                    //return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var msg = "Failed to Add Contact";
                    model.Response = new ResponseModel(msg, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }



        public IActionResult EditContact()
        {
            var model = new ContactsModel();
            return View(model);
        }




        public async Task<IActionResult> UploadContacts()
        {
            var model = new ContactsUploadModel();
            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadContacts(ContactsUploadModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.SaveContactsUploadAsync();
                    var msg = "Congrats! contacts upload is currently being processed! This could take a few minutes .In the meantime you can continue working in DevSkill Email marketting.";
                    model.Response = new ResponseModel("Contacts Upload Added successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //model.Response = new ResponseModel("Contacts Upload added failured.", ResponseType.Failure);
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            return View(model);
        }

        public async Task<JsonResult> GetAllFieldMaps()
        {
            var model = new ContactsUploadModel();
            var data = await model.GetAllFieldMapForSelectAsync();
            return Json(data);
        }
    }
}
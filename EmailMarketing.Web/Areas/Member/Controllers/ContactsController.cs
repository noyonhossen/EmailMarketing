using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Web.Areas.Member.Models.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            var model = new ContactsModel();
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
        public IActionResult AddSingleContact()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult EditContact()
        {
            var model = new ContactsModel();
            return View(model);
        }




        public IActionResult UploadContacts()
        {
            var model = new ContactsUploadModel();
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
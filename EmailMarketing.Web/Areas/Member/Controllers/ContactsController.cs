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
        public IActionResult UploadContacts()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult MapFields()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult ChooseActions()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult Review()
        {
            var model = new ContactsModel();
            return View(model);
        }
        public IActionResult CustomFields()
        {
            var model = new ContactsModel();
            return View(model);
        }
    }
}
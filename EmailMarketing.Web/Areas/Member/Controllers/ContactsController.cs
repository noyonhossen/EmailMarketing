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
    }
}
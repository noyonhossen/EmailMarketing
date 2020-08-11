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
    public class ContactUploadController : Controller
    {
        private readonly ILogger<ContactUploadController> _logger;

        public ContactUploadController(ILogger<ContactUploadController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadContact()
        {
            var model = new ContactUploadModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadContact(ContactUploadModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.SaveContactsUploadAsync();
                    model.Response = new ResponseModel("Contacts Upload successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //model.Response = new ResponseModel("Contacts Upload failured.", ResponseType.Failure);
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }

        #region json helper method
        public async Task<JsonResult> GetAllFieldMaps()
        {
            var model = new ContactUploadModel();
            var data = await model.GetAllFieldMapForSelectAsync();
            return Json(data);
        }

        public async Task<JsonResult> GetAllGroups()
        {
            var model = new ContactUploadModel();
            var data = await model.GetAllGroupForSelectAsync();
            return Json(data);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.AspNetCore.Mvc;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class EmailTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEmailTemplate()
        {
            var model = new CreateEmailTemplateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmailTemplateAsync([Bind(nameof(CreateEmailTemplateModel.EmailTemplateBody),
            nameof(CreateEmailTemplateModel.EmailTemplateName))] CreateEmailTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateEmailTemplate();
                    model.Response = new ResponseModel("Template Added Successfully", Enums.ResponseType.Success);
                    return RedirectToAction("AddEmailTemplate");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, Enums.ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("Template Creation Failed", Enums.ResponseType.Failure);
                }
            }
            return View(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class SMTPController : Controller
    {
        private readonly ILogger<SMTPController> _logger;

        public SMTPController(ILogger<SMTPController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<SMTPModel>();
            return View(model);
        }
        public IActionResult Add()
        {
            var model = new CreateSMTPModel(); 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            [Bind(nameof(CreateSMTPModel.Server),
            nameof(CreateSMTPModel.Port),
            nameof(CreateSMTPModel.SenderName),
            nameof(CreateSMTPModel.SenderEmail),
            nameof(CreateSMTPModel.UserName),
            nameof(CreateSMTPModel.Password),
            nameof(CreateSMTPModel.EnableSSL))] CreateSMTPModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.AddAsync();
                    model.Response = new ResponseModel("SMTP creation successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("SMTP creation failured.", ResponseType.Failure);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditSMTPModel();
            await model.LoadByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind(nameof(EditSMTPModel.Id),
            nameof(CreateSMTPModel.Server),
            nameof(CreateSMTPModel.Port),
            nameof(CreateSMTPModel.SenderName),
            nameof(CreateSMTPModel.SenderEmail),
            nameof(CreateSMTPModel.UserName),
            nameof(CreateSMTPModel.Password),
            nameof(CreateSMTPModel.EnableSSL))] EditSMTPModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await model.UpdateAsync();
                    model.Response = new ResponseModel("SMTP edit successful.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    model.Response = new ResponseModel(ex.Message, ResponseType.Failure);
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("SMTP edit failured.", ResponseType.Failure);
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
                var model = new SMTPModel();
                try
                {
                    var title = await model.DeleteAsync(id);
                    model.Response = new ResponseModel($"Smtp {title} successfully deleted.", ResponseType.Success);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    model.Response = new ResponseModel("SMTP delete failured.", ResponseType.Failure);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetSMTP()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<SMTPModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }
    }
}

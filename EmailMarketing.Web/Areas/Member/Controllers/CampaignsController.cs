using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EmailMarketing.Web.Areas.Member.Enums;

using EmailMarketing.Web.Areas.Member.Models;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CampaignsController : Controller
    {

        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(ILogger<CampaignsController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new CampaignsModel();
            return View(model);
        }
        public async Task<IActionResult> AddCampaign()
        {
            var model = new CreateCampaignModel();
            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            model.EmailTemplateList = await model.GetTemplateByUserIDAsync();
            model.SMTPConfigList = await model.GetAllSMTPConfigByUserIdAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCampaign(CreateCampaignModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await model.SaveCampaignAsync();
                    _logger.LogInformation("Campaing added Successfully");
                    model.Response = new Models.ResponseModel("Campaign Added Successfully!!", Enums.ResponseType.Success);
                    return RedirectToAction("AddCampaign");
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel("Failed to Add Campaign!!", Enums.ResponseType.Failure);
                    _logger.LogError(ex.Message);
                }
            }
            model.GroupSelectList = await model.GetAllGroupForSelectAsync();
            model.EmailTemplateList = await model.GetTemplateByUserIDAsync();
            model.SMTPConfigList = await model.GetAllSMTPConfigByUserIdAsync();
            return View(model);
        }

        public async Task<IActionResult> ViewReport()
        {
            var model = new CampaignsModel();
            await model.LoadAllCampaignSelectListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Export(
            CampaignsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.IsExportAll)
                    {
                        await model.ExportAllCampaign();
                    }
                    else
                    {
                        await model.ExportCampaignWise();
                    }
                    _logger.LogInformation("Succecssfully Added to DownloadQueue. Waiting to Complete to Export");
                }
                catch
                {
                    model.Response = new ResponseModel("Please provide Email", ResponseType.Failure);
                }

            }
            return RedirectToAction("ViewReport");
        }
    }
}
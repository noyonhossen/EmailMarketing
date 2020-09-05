
using Microsoft.AspNetCore.Mvc;
using Autofac;
using EmailMarketing.Web.Areas.Member.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EmailMarketing.Web.Areas.Member.Enums;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.CodeAnalysis.Options;
using EmailMarketing.Web.Core;
using Microsoft.Extensions.Options;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CampaignsController : Controller
    {

        private readonly ILogger<CampaignsController> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        public CampaignsController(ILogger<CampaignsController> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
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
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    model.Response = new ResponseModel(ex.Message, Enums.ResponseType.Failure);
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
        public async Task<IActionResult> ViewCampaigns()
        {

            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            
            var data = await model.GetAllCampaignsAsync(tableModel);
            return Json(data);
        }
        public async Task<IActionResult> ViewCampignWiseReport(int Id)
        {
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            await model.GetCampaignData(Id);
            await model.SetCapaignId(Id);
            return View(model);

        }
        public async Task<IActionResult> ViewDeleveryReport()
        {
            var campaignId = Convert.ToInt32(Request.Query["campaignId"]);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
           
            var data = await model.GetCampaignReportByCampaignIdAsync(tableModel,campaignId);
            return Json(data);
        }
       
        [HttpPost]
        public async Task<IActionResult> Export(
            CampaignsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model._appSettings = _appSettings.Value;
                    if (model.IsExportAll)
                    {
                        await model.ExportAllCampaign();
                    }
                    else
                    {
                        await model.ExportCampaignWise();
                    }
                    _logger.LogInformation("Succecssfully Added to DownloadQueue. Waiting to Complete to Export");
                    model.Response = new ResponseModel("Successfully added to queue. Please wait a while to process", ResponseType.Success);
                }
                catch
                {
                    model.Response = new ResponseModel("Please provide Email", ResponseType.Failure);
                }

            }
            return RedirectToAction("ViewReport");
        }

        [HttpPost]
        public async Task<IActionResult> ActivateCampaign(int id)
        {
            var model = new CampaignsModel();
            try
            {
                var result = await model.ActivateCampaign(id);
                model.Response = new ResponseModel($"{result.Name} { (result.IsProcessing == true ? "successfully  Finished" : "in Processing") }", ResponseType.Success);
                _logger.LogInformation($"Campaign - {result.Name} - Processing Status updated");
            }
            catch(Exception ex)
            {
                model.Response = new ResponseModel("Campaign Processing Status Operation failured.", ResponseType.Failure);
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}

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
        public async Task<IActionResult> ViewCampaigns()
        {

            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            var data = await model.GetAllCampaignsAsync(tableModel);
            return Json(data);
        }
        public IActionResult ViewCampignWiseReport(int Id)
        {
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            model.SetCapaignId(Id);
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
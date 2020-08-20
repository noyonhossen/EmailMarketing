using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public IActionResult Add()
        {
            var model = new CampaignsModel();
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
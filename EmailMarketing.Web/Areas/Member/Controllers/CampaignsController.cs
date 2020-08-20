using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public IActionResult ViewReport()
        {
            var model = new CampaignsModel();
            return View(model);
        }
    }
}
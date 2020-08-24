using System.Threading.Tasks;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using EmailMarketing.Web.Areas.Member.Models;
using System;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CampaignsController : Controller
    {
        public IActionResult Index()
        {
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            return View(model);
        }
        public IActionResult Add()
        {
            var model = new CampaignsModel();
            return View(model);
        }

        public IActionResult ViewReport()
        {
            var model = new CampaignsModel();
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
       

       
    }
}
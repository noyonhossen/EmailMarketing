using System.Threading.Tasks;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using EmailMarketing.Web.Areas.Member.Models;

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
        public IActionResult ViewCampignWiseReport()
        {
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            return View(model);

        }
        public async Task<IActionResult> ViewDeleveryReport()
        {

            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = Startup.AutofacContainer.Resolve<CampaignsModel>();
            var data = await model.GetAllAsync(tableModel);
            return Json(data);
        }
       

       
    }
}
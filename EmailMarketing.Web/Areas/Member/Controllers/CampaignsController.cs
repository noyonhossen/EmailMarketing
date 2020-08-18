using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EmailMarketing.Web.Areas.Member.Models.Campaigns;
using Microsoft.AspNetCore.Mvc;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class CampaignsController : Controller
    {
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

        public IActionResult ViewReport()
        {
            var model = new CampaignsModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Export(
            CampaignsModel model)
        {
            if (ModelState.IsValid)
            {
                await model.CheckSelectOption();

            }
            return RedirectToAction("ViewReport");
        }
    }
}
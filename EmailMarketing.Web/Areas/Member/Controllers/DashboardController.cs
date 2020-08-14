using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmailMarketing.Framework.Entities;
using EmailMarketing.Framework.Services.Contacts;
using EmailMarketing.Web.Areas.Member.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IContactUploadService _contactExcelService;

        public DashboardController(ILogger<DashboardController> logger, IWebHostEnvironment env, IContactUploadService contactExcelService)
        {
            _logger = logger;
            _env = env;
            _contactExcelService = contactExcelService;
        }

        public IActionResult Index()
        {
            var model = new DashboardModel();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
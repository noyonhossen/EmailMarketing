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
        private readonly IContactExcelService _contactExcelService;

        public DashboardController(ILogger<DashboardController> logger, IWebHostEnvironment env, IContactExcelService contactExcelService)
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

        [HttpGet]
        public ViewResult Create()
        {
            return View(new ContactModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.FileUrl != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "lib");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileUrl.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.FileUrl.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                ContactUpload newEmployee = new ContactUpload
                {
                    FileUrl = uniqueFileName,
                    GroupId = model.GroupId,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    //PhotoPath = uniqueFileName
                };

                await _contactExcelService.AddAsync(newEmployee);
                //return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
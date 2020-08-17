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
        //public async Task<IActionResult> GetCampaignReport()
        //{
        //    var model = new CampaignsModel();
        //    model.CampaignReportList = await model.GetAllGroupForSelectAsync();
        //    return View("hello");
        //}
        public async Task<IActionResult> Export()
        {
            var model = new CampaignsModel();
            var campaignReport = await model.GetAllGroupForSelectAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Email";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Column(1).AdjustToContents();
                worksheet.Cell(currentRow, 2).Value = "Delivered Status";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Column(2).AdjustToContents();
                worksheet.Cell(currentRow, 3).Value = "Seen Status";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Column(3).AdjustToContents();
                worksheet.Cell(currentRow, 4).Value = "Send Date";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Column(4).AdjustToContents();
                worksheet.Cell(currentRow, 5).Value = "Seen Date";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Column(5).AdjustToContents();

                foreach (var item in campaignReport)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Email;
                    worksheet.Column(1).AdjustToContents();
                    worksheet.Cell(currentRow, 2).Value = item.IsDelivered == true ? "Yes" : "NO";
                    worksheet.Column(2).AdjustToContents();
                    worksheet.Cell(currentRow, 3).Value = item.IsSeen == true ? "Yes" : "NO";
                    worksheet.Column(3).AdjustToContents();
                    worksheet.Cell(currentRow, 4).Value = ""+item.SendDateTime.ToString();
                    worksheet.Column(4).AdjustToContents();
                    worksheet.Cell(currentRow, 5).Value = item.SeenDateTime.ToString();
                    worksheet.Column(5).AdjustToContents();
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "users.xlsx");
                }
            }
        }
    }
}
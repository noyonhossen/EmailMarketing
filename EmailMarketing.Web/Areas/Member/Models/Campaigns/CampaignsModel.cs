using ClosedXML.Excel;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.Services.Campaigns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Campaigns
{
    public class CampaignsModel : CampaignBaseModel
    {
        public IList<CampaignValueTextModel> CampaignReportList { get; set; }
        public CampaignsModel(ICampaignService campaignService,
            ICurrentUserService currentUserService) : base(campaignService, currentUserService)
        {

        }
        public CampaignsModel() : base()
        {

        }
        public async Task<byte[]> GetAllGroupForSelectAsync()
        {
            var campaignReport = (await _campaignService.GetAllCampaignReportAsync(_currentUserService.UserId))
                                           .Select(x => new CampaignValueTextModel { Value = x.Value, CampaignName = x.CampaignName, Email = x.Email,IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime }).ToList();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Email";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Column(1).AdjustToContents();
                worksheet.Cell(currentRow, 2).Value = "Delivered";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Column(2).AdjustToContents();
                worksheet.Cell(currentRow, 3).Value = "Seen";
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
                    worksheet.Cell(currentRow, 4).Value = "" + item.SendDateTime.ToString();
                    worksheet.Column(4).AdjustToContents();
                    worksheet.Cell(currentRow, 5).Value = item.SeenDateTime.ToString();
                    worksheet.Column(5).AdjustToContents();
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return content;    
                }
            }
        }
    }
}

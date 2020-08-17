using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Contacts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailMarketing.ExcelExportWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IContactExportService _contactExportService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Worker(IHostingEnvironment hostingEnvironment, ILogger<Worker> logger, IContactExportService contactExportService, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _contactExportService = contactExportService;
            _currentUserService = currentUserService;
            _hostingEnvironment = hostingEnvironment;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started at: {DateTime.Now}");
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var result = await _contactExportService.GetDownloadQueue();

                    foreach (var item in result)
                    {

                        var importResult = await _contactExportService.GetDownloadQueueByIdAsync(item.Id);

                        using (var workbook = new XLWorkbook())
                        {
                            var contacts = await _contactExportService.GetAllContactsAsync(item.UserId);

                            var worksheet = workbook.Worksheets.Add("Users");
                            var currentRow = 1;
                            int i = 3;

                            worksheet.Cell(currentRow, 1).Value = "Email";
                            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

                            worksheet.Cell(currentRow, 2).Value = "Group";
                            //worksheet.Cell(currentRow, 2).Style.Border = 
                            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                            
                            var fieldmaplist = contacts.SelectMany(x => x.ContactValueMaps).Select(x => x.FieldMap.DisplayName).Distinct().ToList();


                            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();

                            for(int j=0;j<fieldmaplist.Count();j++)
                            {
                                worksheet.Cell(currentRow, j + 3).Value = fieldmaplist[j];

                                worksheet.Cell(currentRow, j+3).Style.Font.Bold = true;
                                keyValuePairs.Add(fieldmaplist[j], j+3);
                            }
                            

                            foreach (var item1 in contacts)
                            {
                                i++; currentRow++;
                                worksheet.Cell(currentRow, 1).Value = item1.Email;
                                
                                string group = string.Join(", ",item1.ContactGroups.Select(x => x.Group.Name));
                                

                                worksheet.Cell(currentRow, 2).Value = group;

                                for (int j=0;j<item1.ContactValueMaps.Count();j++)
                                {
                                    if(keyValuePairs.ContainsKey(item1.ContactValueMaps.Select(x => x.FieldMap.DisplayName).ToArray()[j]))
                                    {
                                        worksheet.Cell(currentRow, keyValuePairs[item1.ContactValueMaps.Select(x => x.FieldMap.DisplayName).ToArray()[j]]).Value = item1.ContactValueMaps[j].Value;
                                        //worksheet.Cell(currentRow, keyValuePairs[item1.ContactValueMaps.Select(x => x.FieldMap.DisplayName).ToArray()[j]]).Co
                                    }
                                }
                                


                            }
                            //need to change
                            worksheet.Columns("1", "6").AdjustToContents();
                            
                            var memory = new MemoryStream();
                            using (var stream = new FileStream(Path.Combine(item.FileUrl, item.FileName), FileMode.Append))
                            {
                                workbook.SaveAs(stream);
                                //await stream.CopyToAsync(memory);
                            }
                            //workbook.SaveAs("D:\\Working\\Demo.xlsx");
                            

                        }
                        importResult.IsProcessing = false;
                        importResult.IsSucceed = true;
                        importResult.FileUrl = Path.Combine(item.FileUrl, item.FileName);
                        //_contactExportService.UpdateDownloadQueue(importResult);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error message : {ex.Message}");
                }

                await Task.Delay(120000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker stopped at: {DateTime.Now}");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _logger.LogInformation($"Worker disposed at: {DateTime.Now}");
            base.Dispose();
        }
    }
}

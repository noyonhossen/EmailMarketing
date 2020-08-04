using EmailMarketing.Common.Services;
using EmailMarketing.Framework.Services.Smtp;
using EmailMarketing.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Areas.Member.Models.Smtp
{
    public class SMTPModel:SMTPBaseModel
    {
        public SMTPModel(ISmtpService smtpService, IApplicationUserService applicationUserService,
            ICurrentUserService currentUserService):base(smtpService, applicationUserService, currentUserService)
        {
        }

        public SMTPModel() : base() { }

        public async Task<object> GetAllAsync(DataTablesAjaxRequestModel tableModel)
        {
            var result = await _smtpService.GetAllAsync(
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Server","Port","SenderName","SenderEmail","UserName","EnableSSL" }),
                tableModel.PageIndex, tableModel.PageSize);
            var userId = _currentUserService.UserId;
            return new
            {
                recordsTotal = result.Total,
                recordsFiltered = result.TotalFilter,

                data = (from item in result.Items
                        where (item.UserId == userId)
                        select new string[]
                        {
                                    item.Server,
                                    item.Port.ToString(),
                                    item.SenderName,
                                    item.SenderEmail,
                                    item.UserName,
                                    item.EnableSSL.ToString(),
                                    item.Id.ToString()
                        }
                        ).ToArray()

            };
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var group = await _smtpService.DeleteAsync(id);
            return group.Server;
        }
    }
}

using EmailMarketing.Framework.Entities.Campaigns;
using EmailMarketing.Framework.UnitOfWorks.Campaigns;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EmailMarketing.Common.Exceptions;
using EmailMarketing.Framework.Entities.Contacts;

namespace EmailMarketing.Framework.Services.Campaigns
{
    public class CampaignService : ICampaignService
    {
        private ICampaignUnitOfWork _campaignUnitOfWork;
        public CampaignService(ICampaignUnitOfWork campaignUnitOfWork)
        {
            _campaignUnitOfWork = campaignUnitOfWork;
        }
        //public async Task<IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)>> GetAllCampaignReportAsync(Guid? userId)
        //{
        //    var result = (await _campaignUnitOfWork.CampaingReportRepository.GetAsync(x => new { Value = x.Id, CampaignName = x.Campaign.Name, Email = x.Contact.Email, IsDelivered = x.IsDelivered, IsSeen = x.IsSeen, SendDateTime = x.SendDateTime, SeenDateTime = x.SeenDateTime },
        //                                           x => !x.IsDeleted && x.IsActive &&
        //                                           (!userId.HasValue || x.Campaign.UserId == userId.Value), x => x.OrderBy(o => o.Contact.Email), null, true))
        //                                           .Select(x => (Value: x.Value, CampaignName: x.CampaignName, Email: x.Email, IsDelivered: x.IsDelivered, IsSeen: x.IsSeen, SendDateTime: x.SendDateTime, SeenDateTime: x.SeenDateTime)).ToList();
        //    if (result == null) throw new NotFoundException(nameof(CampaignReport), userId);
        //    return result;
        //
        //public async Task<(IList<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)> items, int Total, int TotalFilter)> GetAllCampaignReportAsync(
        //     string searchText,
        //     string orderBy,
        //     int pageIndex,
        //     int pageSize)
        //{
        //    var resultItems = new List<(int Value, string CampaignName, string Email, bool IsDelivered, bool IsSeen, DateTime SendDateTime, DateTime? SeenDateTime)> ();
        //    var resultTotal = 0;
        //    var resultTotalFilter = 0;
        //    var columnsMap = new Dictionary<string, Expression<Func<CampaignReport, object>>>()
        //    {

        //        ["campaignId"] = v => v.CampaignId,
        //        ["seenDateTime"] = v => v.SeenDateTime,
        //        ["sendDateTime"] = v => v.SendDateTime,

        //    };
        //    var result = (await _campaignUnitOfWork.CampaignReportRepository.GetAsync(x => x,
        //                                          x => !x.IsDeleted && x.IsActive &&
        //                                          (!userId.HasValue || x.Campaign.UserId == userId.Value) && (x.CampaignId == campaignId),
        //                                          x => x.OrderBy(o => o.Contact.Email),
        //                                          x => x.Include(y => y.Contact).Include(y => y.Campaign),
        //                                          true));

        //    resultTotal = await query.CountAsync();

        //    //query = query.Where(x => !x.IsDeleted && x.UserRoles.Any(ur => ur.Role.Name == ConstantsValue.UserRoleName.Admin) &&
        //    //    x.Status != EnumApplicationUserStatus.SuperAdmin &&
        //    //    (string.IsNullOrWhiteSpace(searchText) || x.FullName.Contains(searchText) ||
        //    //    x.UserName.Contains(searchText) || x.Email.Contains(searchText)));

        //    resultTotalFilter = await query.CountAsync();
         //  query = query.ApplyOrdering(columnsMap, orderBy);
        //    query = query.ApplyPaging(pageIndex, pageSize);
        //    resultItems = (await query.AsNoTracking().ToListAsync());

        //    return (resultItems, resultTotal, resultTotalFilter);
        //}


        public async Task<(IList<CampaignReport> Items, int Total, int TotalFilter)> GetAllCampaignReportAsync(
          Guid? userId, 
          int campaignId,
          string searchText,
          string orderBy,
          int pageIndex,
          int pageSize)
        {
 

            var result = (await _campaignUnitOfWork.CampaignReportRepository.GetAsync<CampaignReport>(x => x,
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value)  && (x.CampaignId == campaignId) && x.Contact.Email.Contains(searchText),
                                                   x => x.OrderBy(o => o.Contact.Email),
                                                   x => x.Include(y => y.Contact).Include(y => y.Campaign),pageIndex,pageSize,
                                                   true));
            if (result.Items == null) throw new NotFoundException(nameof(CampaignReport), userId);

            return (result.Items, result.Total, result.TotalFilter);
        }

    }
}

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

        public Task<(IList<Campaign> Items, int Total, int TotalFilter)> GetAllCampaignAsync(Guid? userId, string searchText, string orderBy, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        //public async Task<(IList<Campaign> Items, int Total, int TotalFilter)> GetAllCampaignAsync(
        //  Guid? userId,
        //  string searchText,
        //  string orderBy,
        //  int pageIndex,
        //  int pageSize)
        //{
        //    var result = (await _campaignUnitOfWork.CampaignReportRepository.GetAsync(x => x,
        //                                          x => !x.IsDeleted && x.IsActive &&
        //                                          (!userId.HasValue || x.Campaign.UserId == userId.Value) && x.Contact.Email.Contains(searchText),
        //                                          x => x.OrderBy(o => o.Contact.Email),
        //                                          x => x.Include(y => y.Contact).Include(y => y.Campaign), pageIndex, pageSize,
        //                                          true));
        //    if (result.Items == null) throw new NotFoundException(nameof(CampaignReport), userId);

        //    return (result.Items, result.Total, result.TotalFilter);

        //}

        public async Task<(IList<CampaignReport> Items, int Total, int TotalFilter)> GetAllCampaignReportAsync(
          Guid? userId,
          int campaignId,
          string searchText,
          string orderBy,
          int pageIndex,
          int pageSize)
        {
            var result = (await _campaignUnitOfWork.CampaignReportRepository.GetAsync(x => x,
                                                   x => !x.IsDeleted && x.IsActive &&
                                                   (!userId.HasValue || x.Campaign.UserId == userId.Value) && (x.CampaignId == campaignId) && x.Contact.Email.Contains(searchText),
                                                   x => x.OrderBy(o => o.Contact.Email),
                                                   x => x.Include(y => y.Contact).Include(y => y.Campaign), pageIndex, pageSize,
                                                   true));
            if (result.Items == null) throw new NotFoundException(nameof(CampaignReport), userId);

            return (result.Items, result.Total, result.TotalFilter);
        }

        
    }
}

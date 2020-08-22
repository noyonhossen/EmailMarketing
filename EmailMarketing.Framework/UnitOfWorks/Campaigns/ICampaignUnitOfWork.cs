using EmailMarketing.Data;
using EmailMarketing.Framework.Repositories.Campaigns;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.UnitOfWorks.Campaigns
{
    public interface ICampaignUnitOfWork : IUnitOfWork
    {
        public ICampaignRepository CampaignRepository { get; set; }
        public IEmailTemplateRepository EmailTemplateRepository { get; set; }

    }
}

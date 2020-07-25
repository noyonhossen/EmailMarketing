using System;
using EmailMarketing.Common.Services;

namespace EmailMarketing.Web.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

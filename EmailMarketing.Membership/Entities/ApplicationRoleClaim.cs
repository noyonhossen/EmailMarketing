using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class ApplicationRoleClaim
        : IdentityRoleClaim<Guid>
    {
        public ApplicationRoleClaim()
            : base()
        {

        }
    }
}

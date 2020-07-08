using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class ApplicationUserClaim
        : IdentityUserClaim<Guid>
    {
        public ApplicationUserClaim()
            : base()
        {

        }
    }
}

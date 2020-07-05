using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class ApplicationUserToken
        : IdentityUserToken<Guid>
    {
        public ApplicationUserToken()
            : base()
        {

        }
    }
}

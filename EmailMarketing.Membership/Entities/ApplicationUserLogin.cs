using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class ApplicationUserLogin
        : IdentityUserLogin<Guid>
    {
        public ApplicationUserLogin()
            : base()
        {

        }
    }
}

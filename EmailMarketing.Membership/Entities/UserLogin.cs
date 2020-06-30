using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class UserLogin
        : IdentityUserLogin<Guid>
    {
        public UserLogin()
            : base()
        {

        }
    }
}

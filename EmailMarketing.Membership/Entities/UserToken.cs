using System;

using Microsoft.AspNetCore.Identity;

namespace EmailMarketing.Membership.Entities
{
    public class UserToken
        : IdentityUserToken<Guid>
    {
        public UserToken()
            : base()
        {

        }
    }
}

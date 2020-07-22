using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Membership.Constants
{
    public static class ConstantsValue
    {
        public static UserRoleName UserRoleName => new UserRoleName();
    }

    public class UserRoleName
    {
        public string SuperAdmin => "SuperAdmin";
        public string Admin => "Admin";
        public string Member => "Member";
    }
}

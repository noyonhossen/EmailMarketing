using EmailMarketing.Membership.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EmailMarketing.Membership.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public EnumApplicationRoleStatus Status { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public IList<UserRole> UserRoles { get; set; }

        public Role()
            : base()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.UserRoles = new List<UserRole>();
        }

        public Role(string roleName)
            : base(roleName)
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.UserRoles = new List<UserRole>();
        }
    }
}

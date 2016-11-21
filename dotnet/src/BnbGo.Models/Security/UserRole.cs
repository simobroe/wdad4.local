using System;
using System.Collections.Generic;
using OpenIddict;

namespace BnbGo.Models.Security
{
    public class UserRole
    {
        public Guid RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

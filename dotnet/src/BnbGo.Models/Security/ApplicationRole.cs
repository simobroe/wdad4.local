using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenIddict;

namespace BnbGo.Models.Security
{
    public class ApplicationRole : IdentityRole<Guid> 
    {
        // basic user information
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        // basic Create Update and Delete
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        // users with this Role
        public List<UserRole> Users { get; set; }
    }
}
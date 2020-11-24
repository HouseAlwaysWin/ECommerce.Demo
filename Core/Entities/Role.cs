using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Demo.Core.Entities {
    public class Role : IdentityRole<int> {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
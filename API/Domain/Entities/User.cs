using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Demo.API.Domain.Entities {
    public class User : IdentityUser<int> {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
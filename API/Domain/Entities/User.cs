using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Domain.Entities {
    public class User : IdentityUser<int> {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
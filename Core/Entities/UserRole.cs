using Microsoft.AspNetCore.Identity;

namespace ECommerce.Demo.Core.Entities {
    public class UserRole : IdentityUserRole<int> {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
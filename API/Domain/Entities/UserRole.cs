using Microsoft.AspNetCore.Identity;

namespace ECommerce.Demo.API.Domain.Entities {
    public class UserRole : IdentityUserRole<int> {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
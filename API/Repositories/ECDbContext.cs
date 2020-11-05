using ECommerce.Demo.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Demo.API.Repositories {
    public class ECDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>> {
        public ECDbContext (DbContextOptions<ECDbContext> options) : base (options) { }
        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.Entity<User> (e => {
                e.ToTable (name: "User");
            });

            builder.Entity<Role> (e => {
                e.ToTable (name: "Role");
            });

            builder.Entity<UserRole> (e => {
                e.ToTable (name: "UserRole");
                e.HasKey (ur => new { ur.UserId, ur.RoleId });
                e.HasOne (ur => ur.Role)
                    .WithMany (r => r.UserRoles)
                    .HasForeignKey (ur => ur.RoleId)
                    .IsRequired ();
                e.HasOne (ur => ur.User)
                    .WithMany (ur => ur.UserRoles)
                    .HasForeignKey (ur => ur.UserId)
                    .IsRequired ();
            });

            builder.Entity<IdentityUserClaim<int>> (e => {
                e.ToTable ("UserClaim");
            });

            builder.Entity<IdentityUserLogin<int>> (e => {
                e.ToTable ("UserLogin");
                e.HasKey (ul => ul.UserId);
            });

            builder.Entity<IdentityUserClaim<int>> (e => {
                e.ToTable ("UserClaim");
                e.HasKey (uc => uc.Id);
            });

            builder.Entity<IdentityRoleClaim<int>> (e => {
                e.ToTable ("RoleClaims");
                e.HasKey (rc => rc.RoleId);
            });

            builder.Entity<IdentityUserToken<int>> (e => {
                e.ToTable ("UserTokens");
                e.HasKey (ut => ut.UserId);
            });

        }
    }
}
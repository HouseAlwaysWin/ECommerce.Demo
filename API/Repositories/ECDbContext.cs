using API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories {
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

            builder.Entity<IdentityUserClaim<string>> (e => {
                e.ToTable ("UserClaim");
            });

            builder.Entity<IdentityUserLogin<string>> (e => {
                e.ToTable ("UserLogin");
            });

            builder.Entity<IdentityRoleClaim<string>> (e => {
                e.ToTable ("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>> (e => {
                e.ToTable ("UserTokens");
            });

        }
    }
}
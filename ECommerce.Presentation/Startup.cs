using System;
using System.ComponentModel.Design;
using ECommerce.Repository.Data;
using ECommerce.Service.Services;
using ECommerce.Service.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Presentation {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("DefaultConnection"),
                    b => b.MigrationsAssembly ("ECommerce.Presentation")));

            services.AddIdentity<IdentityUser, IdentityRole> (
                    options => {
                        options.SignIn.RequireConfirmedAccount = false;
                        options.SignIn.RequireConfirmedEmail = false;
                        options.SignIn.RequireConfirmedPhoneNumber = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Lockout.AllowedForNewUsers = false;
                    }
                )
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            services.AddControllersWithViews ();
            services.AddRazorPages ();
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_3_0);

            services.ConfigureApplicationCookie (options => {
                options.LoginPath = $"/Account/Login";
                options.LogoutPath = $"/Account/Logout";
                options.AccessDeniedPath = $"/Account/AccessDenied";
            });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            // services.AddSingleton<IEmailSender, EmailSender> ();
            services.AddTransient<UserManager<IdentityUser>> ();
            services.AddTransient<SignInManager<IdentityUser>> ();
            services.AddTransient<IAccountService, AccountService> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages ();
            });
        }
    }
}
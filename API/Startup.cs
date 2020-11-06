using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using API.Services;
using AutoMapper;
using ECommerce.Demo.API.Controllers;
using ECommerce.Demo.API.Domain.Entities;
using ECommerce.Demo.API.Repositories;
using ECommerce.Demo.API.Services;
using ECommerce.Demo.API.SqlServerRepo.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCore.AutoRegisterDi;
using NLog.Extensions.Logging;

namespace ECommerce.Demo.API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            IdentityBuilder builder = services.AddIdentityCore<User> (opt => {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });
            builder = new IdentityBuilder (builder.UserType, typeof (Role), builder.Services);
            builder.AddEntityFrameworkStores<ECDbContext> ();
            builder.AddRoleValidator<RoleValidator<Role>> ();
            builder.AddRoleManager<RoleManager<Role>> ();
            builder.AddSignInManager<SignInManager<User>> ();
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    // IssuerSigningKey = new SymmetricSecurityKey ( Encoding.ASCII.GetBytes (Configuration.GetSection ("AppSetting:Token").Value)),
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes ("Test")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    };
                });

            services.AddControllers (options => {
                var policy = new AuthorizationPolicyBuilder ().RequireAuthenticatedUser ().Build ();
                options.Filters.Add (new AuthorizeFilter (policy));
            }).AddNewtonsoftJson (opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper (typeof (UserService).Assembly);
            services.AddDbContext<ECDbContext> (x => x.UseSqlServer (Configuration.GetConnectionString ("Default")));
            services.AddCors ();

            services.AddSingleton<IUnitOfWork> (u => new UnitOfWork<SqlConnection> (Configuration.GetConnectionString ("Default")));
            services.AddSingleton<IProductRepository, ProductRepository<SqlConnection>> ();
            services.AddSingleton<IUserRepository, UserRepository<SqlConnection>> ();
            services.AddScoped<IProductService, ProductService> ();
            services.AddScoped<IUserService, UserService> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}
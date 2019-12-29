// using System;
// using ECommerce.Presentation.Areas.Identity.Data;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.UI;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;

// [assembly: HostingStartup(typeof(ECommerce.Presentation.Areas.Identity.IdentityHostingStartup))]
// namespace ECommerce.Presentation.Areas.Identity
// {
//     public class IdentityHostingStartup : IHostingStartup
//     {
//         public void Configure(IWebHostBuilder builder)
//         {
//             // builder.ConfigureServices((context, services) => {
//             //     services.AddDbContext<IdentityDataContext>(options =>
//             //         options.UseSqlServer(
//             //             context.Configuration.GetConnectionString("IdentityDataContextConnection")));

//             //     services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//             //         .AddEntityFrameworkStores<IdentityDataContext>();
//             // });
//         }
//     }
// }
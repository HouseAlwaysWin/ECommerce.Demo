using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace API {
    public class Program {
        public static void Main (string[] args) {
            var config = new ConfigurationBuilder ()
                .SetBasePath (System.IO.Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                .Build ();

            NLog.LogManager.Configuration = new NLogLoggingConfiguration (config.GetSection ("NLog"));

            var logger = NLog.Web.NLogBuilder.ConfigureNLog (LogManager.Configuration).GetCurrentClassLogger ();
            try {
                CreateHostBuilder (args).Build ().Run ();
            } catch (Exception ex) {
                logger.Error (ex);
            } finally {
                NLog.LogManager.Shutdown ();
            }
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            })
            .ConfigureLogging (logging => {
                logging.ClearProviders ();
                logging.SetMinimumLevel (Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .UseNLog ();
    }
}
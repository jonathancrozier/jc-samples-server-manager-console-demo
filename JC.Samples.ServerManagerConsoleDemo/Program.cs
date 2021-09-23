using JC.Samples.ServerManagerConsoleDemo.Services;
using JC.Samples.ServerManagerConsoleDemo.Services.Interfaces;
using Microsoft.Web.Administration;
using Serilog;
using System;

namespace JC.Samples.ServerManagerConsoleDemo
{
    /// <summary>
    /// Main Program class.
    /// </summary>
    class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command-line arguments</param>
        static void Main(string[] args)
        {
            // Configure logging.
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Console()
               .CreateLogger();

            // Try out the IIS site services.
            using ISiteServices services = new SiteServices();
            
            string siteName = "Default Web Site";

            Site site    = services.GetSite(siteName);
            bool running = services.SiteIsRunning(siteName);
            bool stopped = services.StopSite(siteName);
            bool started = services.StartSite(siteName);

            // Inform the user that the program has completed.
            Console.WriteLine();
            Console.Write("Press any key to exit...");
            Console.ReadKey(true);
        }

        #endregion
    }
}
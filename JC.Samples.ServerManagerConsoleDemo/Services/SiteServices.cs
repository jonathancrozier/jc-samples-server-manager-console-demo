using JC.Samples.ServerManagerConsoleDemo.Services.Interfaces;
using Microsoft.Web.Administration;
using Serilog;
using System;
using System.Linq;

namespace JC.Samples.ServerManagerConsoleDemo.Services
{
    /// <summary>
    /// Provides IIS website services.
    /// </summary>
    public class SiteServices : ISiteServices
    {
        #region Readonlys

        private readonly ServerManager _server;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SiteServices() => _server = new ServerManager();

        #endregion

        #region Methods

        /// <summary>
        /// Gets a <see cref="Site"/> object based on the specified site name.
        /// </summary>
        /// <param name="siteName">The site name</param>
        /// <returns><see cref="Site"/></returns>
        public Site GetSite(string siteName)
        {
            Log.Verbose("Getting site named: {0}", siteName);
            
            Site site = _server.Sites.FirstOrDefault(s => s.Name.Equals(siteName, StringComparison.OrdinalIgnoreCase));

            if (site != null)
            {
                Log.Verbose("Found site named: {0}", siteName);
            }
            else
            {
                Log.Warning("Failed to find site named: {0}", siteName);
            }

            return site;
        }

        /// <summary>
        /// Checks if a site with the specified name is running.
        /// </summary>
        /// <param name="siteName">The site name</param>
        /// <returns>True if the site is running, otherwise false</returns>
        public bool SiteIsRunning(string siteName)
        {
            Site site = GetSite(siteName);

            bool siteIsRunning = site?.State == ObjectState.Started;

            Log.Verbose("The '{0}' site {1}", siteName, siteIsRunning ? "is running" : "is not running");

            return siteIsRunning;
        }

        /// <summary>
        /// Starts the site with the specified name, if it is not already running.
        /// </summary>
        /// <param name="siteName">The site name</param>
        /// <returns>True if the site was started successfully, otherwise false</returns>
        public bool StartSite(string siteName)
        {
            Site site = GetSite(siteName);

            if (site == null) return false;

            bool started = false;

            if (site.State != ObjectState.Started)
            {
                Log.Verbose("Starting site named: {0}", siteName);
                site.Start();

                started = site.State == ObjectState.Started;

                if (started)
                {
                    Log.Verbose("Started site named: {0}", siteName);
                }
                else
                {
                    Log.Warning("Failed to start site named: {0}", siteName);
                }
            }
            else
            {
                Log.Verbose("Site named '{0}' is already started", siteName);

                started = true;
            }

            return started;
        }

        /// <summary>
        /// Stops the site with the specified name, if it is not already stopped.
        /// </summary>
        /// <param name="siteName">The site name</param>
        /// <returns>True if the site was stopped successfully, otherwise false</returns>
        public bool StopSite(string siteName)
        {
            Site site = GetSite(siteName);

            if (site == null) return false;

            bool stopped = false;

            if (site.State != ObjectState.Stopped)
            {
                Log.Verbose("Stopping site named: {0}", siteName);
                site.Stop();

                stopped = site.State == ObjectState.Stopped;

                if (stopped)
                {
                    Log.Verbose("Stopped site named: {0}", siteName);
                }
                else
                {
                    Log.Warning("Failed to stop site named: {0}", siteName);
                }
            }
            else
            {
                Log.Verbose("Site named '{0}' already stopped", siteName);

                stopped = true;
            }

            return stopped;
        }

        #region Implements IDisposable

        #region Private Dispose Fields

        private bool _disposed;

        #endregion

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Take this object off the finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up any resources being used.
        /// </summary>
        /// <param name="disposing">Whether or not we are disposing</param>
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _server?.Dispose();
                }

                // Dispose any unmanaged resources here...

                _disposed = true;
            }
        }

        #endregion

        #endregion
    }
}
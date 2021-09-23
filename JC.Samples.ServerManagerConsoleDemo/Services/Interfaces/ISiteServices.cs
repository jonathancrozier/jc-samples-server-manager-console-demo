using Microsoft.Web.Administration;
using System;

namespace JC.Samples.ServerManagerConsoleDemo.Services.Interfaces
{
    /// <summary>
    /// IIS website services interface.
    /// </summary>
    public interface ISiteServices : IDisposable
    {
        #region Methods

        Site GetSite(string siteName);
        bool SiteIsRunning(string siteName);
        bool StartSite(string siteName);
        bool StopSite(string siteName);

        #endregion
    }
}
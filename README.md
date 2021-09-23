# Samples - ServerManager Console Demo

An example of how to use the `ServerManager` class from the **Microsoft.Web.Administration** library to stop and start IIS websites.

The `ServerManager` logic is enscapsulated within the custom `SiteServices` class.

```csharp
using ISiteServices services = new SiteServices();
 
string siteName = "Default Web Site";
 
Site site    = services.GetSite(siteName);
bool running = services.SiteIsRunning(siteName);
bool stopped = services.StopSite(siteName);
bool started = services.StartSite(siteName);
```

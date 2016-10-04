using System.Diagnostics;
using System.Linq;

namespace WebSiteExtractor
{
    public static class ExternalBrowserController
    {
        public static bool BrowserOpened;

        public static void Navigate(string address)
        {
            if (!BrowserOpened)
            {
                if (Process.GetProcesses().Any(p => p.ProcessName.Contains("firefox")))
                {
                    BrowserOpened = true;
                    Process.Start("firefox.exe", address);
                }
                else
                {
                    Process.Start("firefox.exe", address);
                    System.Threading.Thread.Sleep(5000);
                    BrowserOpened = true;
                }
            }
            else
                Process.Start("firefox.exe", address);
        }
    }
}

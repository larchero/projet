using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using SpotifyAPI.Web.Models;
using static System.Net.WebRequestMethods;

namespace SpotifyAPI.Web.Auth
{
    internal static class AuthUtil
    {
        public static void OpenBrowser(string url)
        {
#if NETSTANDARD2_0
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Console.WriteLine(url);
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
#else
            url = url.Replace("&", "^&");

            Process.Start(new ProcessStartInfo("cmd", $"/c start /MIN {url} /MIN"));
           

#endif
        }

        
        
    }
}
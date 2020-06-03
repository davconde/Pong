using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong {
    public class WebBrowser {
        public static void LoadWebsite(string url) {
#if WINDOWS
            try {
                System.Diagnostics.ProcessStartInfo webPage = new System.Diagnostics.ProcessStartInfo(url);
                System.Diagnostics.Process.Start(webPage);
            } catch { }
#elif ANDROID
            var uri = Android.Net.Uri.Parse(url);
            var intent = new Android.Content.Intent(Android.Content.Intent.ActionView, uri);
            Game1.Activity.StartActivity(intent);
#endif
        }
    }
}

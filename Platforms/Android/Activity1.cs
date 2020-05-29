using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Pong;

namespace Android {
    [Activity(Label = "Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity {
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            var g = new Game1();
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();

            SystemUiFlags flags = SystemUiFlags.HideNavigation
                                  | SystemUiFlags.Fullscreen
                                  | SystemUiFlags.ImmersiveSticky
                                  | SystemUiFlags.Immersive;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)flags;
            Immersive = true;
        }
    }
}


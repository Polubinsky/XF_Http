using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using Xamarin.Forms;
using Acr.UserDialogs;
using CarouselView.FormsPlugin.Android;
using Android.Runtime;
using Plugin.Permissions;
using HockeyApp.Android;
using HockeyApp.Android.Utils;

namespace UkrGo.Droid
{
    [Activity(Label = "UkrGo", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string AppID = "7ba4386fd27844d19c2e8104abad75a0";

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            CarouselViewRenderer.Init();
            UserDialogs.Init(() => (Activity)Forms.Context);



           // CrashManager.Register(this, AppID);



            LoadApplication(new App());
           

        }

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, AppID);
        }

        public override void OnTrimMemory(TrimMemory level)
        {
            ImageService.Instance.InvalidateMemoryCache();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}


using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;
using Xamarin.Forms;
using Acr.UserDialogs;
using CarouselView.FormsPlugin.Android;
using FormsPinView.Droid;

namespace UkrGo.Droid
{
    [Activity(Label = "UkrGo", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            PinItemViewRenderer.Init();
            CarouselViewRenderer.Init();
            FAB.Droid.FloatingActionButtonRenderer.InitControl();
            UserDialogs.Init(() => (Activity)Forms.Context);

            LoadApplication(new App());
           

        }

        public override void OnTrimMemory(TrimMemory level)
        {
            ImageService.Instance.InvalidateMemoryCache();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }

       
    }
}


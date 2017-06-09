using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UkrGo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(ZoomableWebViewRenderer))]
namespace UkrGo.Droid.Renderers
{
    
    public class ZoomableWebViewRenderer : WebViewRenderer
    {

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Control != null)
            {
                Control.Settings.BuiltInZoomControls = true;
                Control.Settings.DisplayZoomControls = true;
            }
            base.OnElementPropertyChanged(sender, e);
        }

    }
}
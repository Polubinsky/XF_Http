using System.ComponentModel;
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
using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using UkrGo.Controls.PinViewControl;
using Xamarin.Forms;
using UkrGo.Droid.Renderers;
using AView = Android.Views.View;
using Xamarin.Forms.Platform.Android;
using UkrGo.Droid.Controls;

[assembly: ExportRenderer(typeof(PinItemView), typeof(PinItemViewRenderer))]
namespace UkrGo.Droid.Renderers
{
    public class PinItemViewRenderer : ViewRenderer<PinItemView, AView>
    {
        private RippleButton _button;

        public static void Init()
        {
            //var t = typeof(PinItemViewRenderer);
        }

        public PinItemViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PinItemView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var sideSize = (int)convertDpToPixel(44);

                    _button = new RippleButton(Context);
                    _button.SetWidth(sideSize);
                    _button.SetHeight(sideSize);
                    //_button.SetBackgroundColor(AColor.Red);
                    _button.SetBackgroundResource(Resource.Drawable.bkg_roundedview);
                    _button.Text = Element.Text;
                    _button.Gravity = GravityFlags.Center;
                    _button.OnClick += (sender, args) =>
                    {
                        Element?.Command?.Execute(Element?.CommandParameter);
                    };

                    FrameLayout frame = new FrameLayout(Context);
                    FrameLayout.LayoutParams @params = new FrameLayout.LayoutParams(sideSize, sideSize);
                    @params.Gravity = GravityFlags.Center;
                    frame.AddView(_button, @params);

                    SetNativeControl(frame);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private float convertDpToPixel(float dp)
        {
            float density = Context.Resources.DisplayMetrics.Density;
            return (int)Math.Round((float)dp * density);
        }
    }
}
using UkrGo.Extensions;
using Xamarin.Forms;

namespace UkrGo.Controls
{

    public class PinchToZoomContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;
        double originalWidth = 0;
        double originalHeight = 0;

        public PinchToZoomContainer()
        {
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var s = (ContentView)sender;

            // do not allow pan if the image is in its intial size
            if (currentScale == 1)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    double xTrans = xOffset + e.TotalX, yTrans = yOffset + e.TotalY;
                    // do not allow verical scorlling unless the image size is bigger than the screen
                    s.Content.TranslateTo(xTrans, yTrans, 0, Easing.Linear);
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    xOffset = s.Content.TranslationX;
                    yOffset = s.Content.TranslationY;

                    // center the image if the width of the image is smaller than the screen width
                    if (originalWidth * currentScale < App.ScreenWidth && App.ScreenWidth > App.ScreenHeight)
                        xOffset = (App.ScreenWidth - originalWidth * currentScale) / 2 - s.Content.X;
                    else
                        xOffset = System.Math.Max(System.Math.Min(0, xOffset), -System.Math.Abs(originalWidth * currentScale - App.ScreenWidth));

                    // center the image if the height of the image is smaller than the screen height
                    if (originalHeight * currentScale < App.ScreenHeight && App.ScreenHeight > App.ScreenWidth)
                        yOffset = (App.ScreenHeight - originalHeight * currentScale) / 2 - s.Content.Y;
                    else
                        //yOffset = System.Math.Max(System.Math.Min((originalHeight - (ScreenHeight)) / 2, yOffset), -System.Math.Abs((originalHeight * currentScale - ScreenHeight - (originalHeight - ScreenHeight) / 2)) + (NavBar.Height + App.StatusBarHeight));
                        yOffset = System.Math.Max(System.Math.Min((originalHeight - (App.ScreenHeight)) / 2, yOffset), -System.Math.Abs((originalHeight * currentScale - App.ScreenHeight - (originalHeight - App.ScreenHeight) / 2)));

                    // bounce the image back to inside the bounds
                    s.Content.TranslateTo(xOffset, yOffset, 500, Easing.BounceOut);
                    break;
            }
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            var s = (ContentView)sender;

            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = s.Content.Scale;

                s.Content.AnchorX = 0;
                s.Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {

                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = System.Math.Max(1, currentScale);
                currentScale = System.Math.Min(currentScale, 5);

                //scaleLabel.Text = "Scale: " + currentScale.ToString ();

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = s.Content.X + xOffset;
                double deltaX = renderedX / App.ScreenWidth;
                double deltaWidth = App.ScreenWidth / (s.Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = s.Content.Y + yOffset;

                double deltaY = renderedY / App.ScreenHeight;
                double deltaHeight = App.ScreenHeight / (s.Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * s.Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * s.Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                var transX = targetX.Clamp(-s.Content.Width * (currentScale - 1), 0);
                var transY = targetY.Clamp(-s.Content.Height * (currentScale - 1), 0);


                s.Content.TranslateTo(transX, transY, 0, Easing.Linear);
                // Apply scale factor.
                s.Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation applied during the pan
                xOffset = s.Content.TranslationX;
                yOffset = s.Content.TranslationY;

                // center the image if the width of the image is smaller than the screen width
                if (originalWidth * currentScale < App.ScreenWidth && App.ScreenWidth > App.ScreenHeight)
                    xOffset = (App.ScreenWidth - originalWidth * currentScale) / 2 - s.Content.X;
                else
                    xOffset = System.Math.Max(System.Math.Min(0, xOffset), -System.Math.Abs(originalWidth * currentScale - App.ScreenWidth));

                // center the image if the height of the image is smaller than the screen height
                if (originalHeight * currentScale < App.ScreenHeight && App.ScreenHeight > App.ScreenWidth)
                    yOffset = (App.ScreenHeight - originalHeight * currentScale) / 2 - s.Content.Y;
                else
                    yOffset = System.Math.Max(System.Math.Min((originalHeight - App.ScreenHeight) / 2, yOffset), -System.Math.Abs(originalHeight * currentScale - App.ScreenHeight - (originalHeight - App.ScreenHeight) / 2));

                // bounce the image back to inside the bounds
                s.Content.TranslateTo(xOffset, yOffset, 500, Easing.BounceOut);
            }
        }

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height); //must be called

        //    if (width != -1 && (ScreenWidth != width || ScreenHeight != height))
        //    {
        //        ResetLayout(width, height);

        //        originalWidth = initialLoad ?
        //            ImageWidth >= 960 ?
        //               App.ScreenWidth > 320
        //                    ? 768
        //                    : 320
        //                : ImageWidth / 3
        //            : imageContainer.Content.Width / imageContainer.Content.Scale;

        //        var normalizedHeight = ImageWidth >= 960 ?
        //                App.ScreenWidth > 320 ? ImageHeight / (ImageWidth / 768)
        //                : ImageHeight / (ImageWidth / 320)
        //            : ImageHeight / 3;

        //        originalHeight = initialLoad ?
        //            normalizedHeight : (imageContainer.Content.Height / imageContainer.Content.Scale);

        //        ScreenWidth = width;
        //        ScreenHeight = height;

        //        xOffset = imageContainer.TranslationX;
        //        yOffset = imageContainer.TranslationY;

        //        currentScale = imageContainer.Scale;

        //        if (initialLoad)
        //            initialLoad = false;
        //    }
        //}


    }
}


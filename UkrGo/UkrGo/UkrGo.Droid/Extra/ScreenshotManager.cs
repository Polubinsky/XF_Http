using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Widget;
using UkrGo.Droid.Extra;
using UkrGo.Interfaces;
using Android.Media;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Android.Util;

[assembly: Xamarin.Forms.Dependency(typeof(ScreenshotManager))]
namespace UkrGo.Droid.Extra
{
    public class ScreenshotManager : IScreenshotManager
    {
        private MediaActionSound _mediaSound;

        public static Activity Activity { get; set; }

        public async Task<byte[]> CaptureAsync()
        {
            
            Activity = CrossCurrentActivity.Current.Activity;
            await Task.Delay(50);
            if (Activity == null)
            {
                throw new Exception("You have to set ScreenshotManager.Activity in your Android project");
            }

            LoadShutterSound();

            var view = Activity.Window.DecorView;
            view.DrawingCacheEnabled = true;

            Bitmap bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                await bitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, 30, stream);
                bitmapData = stream.ToArray();
            }

            view.DrawingCacheEnabled = false;
            _mediaSound.Play(MediaActionSoundType.ShutterClick);
            return bitmapData;
        }

        public void SavePictureToDisk(string filename, byte[] imageData)
        {
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = filename + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png";
            string filePath = System.IO.Path.Combine(pictures, name);
            try
            {
                System.IO.File.WriteAllBytes(filePath, imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));
                Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }

        private bool LoadShutterSound()
        {
            try
            {
                _mediaSound = new MediaActionSound();
                _mediaSound.LoadAsync(MediaActionSoundType.ShutterClick);

                return true;
            }
            catch (Java.Lang.Exception error)
            {
                Log.WriteLine(LogPriority.Error, error.Source, error.Message);
            }

            return false;
        }

    }
}
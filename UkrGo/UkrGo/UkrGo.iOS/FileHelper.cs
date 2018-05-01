using System;
using UkrGo.Interfaces;
using UkrGo.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace UkrGo.iOS
{
    
        public class FileHelper : IFileHelper
        {
            public string GetLocalFilePath(string filename)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, filename);
            }
        }

}

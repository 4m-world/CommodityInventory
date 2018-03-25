using System;
using System.IO;
using MyInventoryApp.Droid.Services;
using MyInventoryApp.Services.FileHelper;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MyInventoryApp.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}

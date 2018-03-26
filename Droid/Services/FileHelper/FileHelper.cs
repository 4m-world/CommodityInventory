using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
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

        public Task<string> ReadText(string filename)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentPath, filename);
            if (File.Exists(filePath))
            {
                using (var reader = File.OpenText(filePath))
                {
                    return reader.ReadToEndAsync();
                }
            }

            return Task.FromResult(string.Empty);
        }

        public async Task<string> SaveText(string filename, string text)
        {
            //var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var documentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

            var filePath = Path.Combine(documentPath, filename);

            byte[] encodedText = Encoding.Unicode.GetBytes(text);  

            Int32 bufferSize = 4096;
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/using-async-for-file-access
            using (FileStream sourceStream = new FileStream(filePath,
                                                            FileMode.Append, 
                                                            FileAccess.Write, 
                                                            FileShare.None,
                                                            bufferSize, 
                                                            useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);


                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));

                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
            };

            return filePath;
        }
    }
}

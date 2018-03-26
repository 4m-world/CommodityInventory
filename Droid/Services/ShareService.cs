using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using MyInventoryApp.Droid.Services;
using MyInventoryApp.Services.Share;
using Xamarin.Forms;

[assembly: Dependency(typeof(ShareService))]
namespace MyInventoryApp.Droid.Services
{
    public class ShareService : IShareService
    {
        readonly Context _context;

        public ShareService()
        {
            _context = Android.App.Application.Context;
        }

        public Task Show(string title, string message, string filePath)
        {
            var extension = filePath.Substring(filePath.LastIndexOf(".", StringComparison.InvariantCultureIgnoreCase) + 1)
                                    .ToLowerInvariant();
            var contentType = string.Empty;

            switch (extension)
            {
                case "json":
                    contentType = "text/json";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = "application/octetstream";
                    break;
            }

            var intent = new Intent(Intent.ActionSend);
            intent.SetType(contentType);
            intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse("file://" + filePath));
            intent.PutExtra(Intent.ExtraText, string.Empty);
            intent.PutExtra(Intent.ExtraSubject, message ?? string.Empty);

            var chooserIntent = Intent.CreateChooser(intent, title ?? string.Empty);
            chooserIntent.SetFlags(ActivityFlags.ClearTop);
            chooserIntent.SetFlags(ActivityFlags.NewTask);
            _context.StartActivity(chooserIntent);

            return Task.FromResult(true);
        }
    }
}

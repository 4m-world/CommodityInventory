
using System.Threading.Tasks;

namespace MyInventoryApp.Services.FileHelper
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string fileName);

        Task<string> SaveText(string filename, string text);

        Task<string> ReadText(string filename);
    }
}

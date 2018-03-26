using System.Threading.Tasks;

namespace MyInventoryApp.Services.Share
{
    public interface IShareService
    {
        Task Show(string title, string message, string filePath);
    }
}

using System.Threading.Tasks;

namespace MyInventoryApp.Services.Dialog
{
    public interface IDialogService
    {
        Task Show(string message);
    }
}

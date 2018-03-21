using System;
using System.Threading.Tasks;

namespace MyInventoryApp.Services
{
    public interface ILoginService
    {
        Task<bool> Authenticate(string username, string password);
    }
}

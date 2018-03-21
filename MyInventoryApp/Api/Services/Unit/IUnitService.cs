using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyInventoryApp.Api.Services.Unit
{
    public interface IUnitService
    {
        Task<ObservableCollection<Models.Unit>> GetUnitsAsync(string token);

        Task<Models.Unit> GetUnit(int id, string token);

        Task<Models.Unit> AddUnit(Models.Unit unit, string token);
    }
}

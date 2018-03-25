using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Database;
using MyInventoryApp.Extensions;

namespace MyInventoryApp.Api.Services.Unit
{
    public class LocalUnitService : IUnitService
    {

        public async Task<Models.Unit> AddUnit(Models.Unit unit, string token = "")
        {
            var id = await CommodityDatabase.Instance
                                            .SaveUnitAsync(unit);
            unit.Id = id;
            return unit;
        }

        public async Task DeleteUnit(int id, string token = "")
        {
            var unit = await CommodityDatabase.Instance
                                        .GetUnitAsync(id);
            if (unit != null)
                await CommodityDatabase.Instance
                                       .DeleteUnitAsync(unit);
        }

        public async Task<Models.Unit> GetUnit(int id, string token = "")
        {
            return await CommodityDatabase.Instance
                                          .GetUnitAsync(id);
        }

        public async Task<ObservableCollection<Models.Unit>> GetUnitsAsync(string token = "", int pageIndex = 0, int pageSize = 20)
        {
            return (await CommodityDatabase.Instance
                    .GetUnitsAsync(pageIndex, pageSize))
                .ToObservableCollection();
                
        }

        public async Task<Models.Unit> UpdateUnit(Models.Unit unit, string token = "")
        {
            await CommodityDatabase.Instance
                                   .SaveUnitAsync(unit);

            return unit;
        }
    }
}

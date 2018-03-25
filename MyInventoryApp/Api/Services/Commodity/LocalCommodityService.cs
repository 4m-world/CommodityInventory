using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Database;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Extensions;

namespace MyInventoryApp.Api.Services.Commodity
{
    public class LocalCommodityService : ICommodityService
    {
        public async Task<CommodityItem> AddCommodityItemAsync(CommodityItem commodity, string token = "")
        {
            var id = await CommodityDatabase.Instance.SaveCommodityAsync(commodity);
            commodity.Id = id;
            return commodity;
        }

        public async Task DeleteCommodityItemAsync(int id, string token = "")
        {
            var commodity = await CommodityDatabase.Instance.GetCommodityAsync(id);
            if (commodity != null)
                await CommodityDatabase.Instance.DeleteCommodityAsync(commodity);
        }

        public async Task<ObservableCollection<CommodityItem>> GetCommoditiesAsync(string token = "", int pageIndex = 0, int pageSize = 20)
        {
            return (await CommodityDatabase.Instance
                                    .GetCommoditiesAsync(pageIndex, pageSize)
                   ).ToObservableCollection();
        }

        public async Task<CommodityItem> GetCommodityItemAsync(int id, string token = "")
        {
            return await CommodityDatabase.Instance
                                          .GetCommodityAsync(id);
        }

        public Task<ObservableCollection<CommodityItem>> SearchAsync(string term, string token = "")
        {
            throw new NotImplementedException();
        }

        public async Task<CommodityItem> UpdateCommodityItemAsync(CommodityItem commodity, string token = "")
        {
            await CommodityDatabase.Instance.SaveCommodityAsync(commodity);
            return commodity;
        }
    }
}

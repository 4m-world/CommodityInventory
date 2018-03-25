using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Models;

namespace MyInventoryApp.Api.Services.Commodity
{
    public interface ICommodityService
    {
        Task<ObservableCollection<CommodityItem>> GetCommoditiesAsync(string token = "", int pageIndex = 0, int pageSize = 20);
        Task<CommodityItem> GetCommodityItemAsync(int id, string token = "");
        Task<CommodityItem> UpdateCommodityItemAsync(CommodityItem commodity, string token = "");
        Task<CommodityItem> AddCommodityItemAsync(CommodityItem commodity, string token = "");
        Task<ObservableCollection<CommodityItem>> SearchAsync(string term, string token = "");
        Task DeleteCommodityItemAsync(int id, string token = "");
    }
}

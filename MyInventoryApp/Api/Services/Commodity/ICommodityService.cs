using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Models;

namespace MyInventoryApp.Api.Services.Commodity
{
    public interface ICommodityService
    {
        Task<ObservableCollection<CommodityItem>> GetCommoditiesAsync(string token);
        Task<CommodityItem> GetCommodityItemAsync(string id, string token);
        Task<CommodityItem> UpdateCommodityItemAsync(CommodityItem commodity, string token);
        Task<CommodityItem> AddCommodityItemAsync(CommodityItem commodity, string token);
        Task<ObservableCollection<CommodityItem>> SearchAsync(string term, string token);
        Task DeleteCommodityItemAsync(string id, string token);
    }
}

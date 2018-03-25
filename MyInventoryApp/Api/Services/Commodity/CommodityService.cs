using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.RequestProvider;
using MyInventoryApp.Extensions;

namespace MyInventoryApp.Api.Services.Commodity
{
    public class CommodityService : ICommodityService
    {
        readonly IRequestProvider _requestProvider;
        const string ApiUrlBase = "api/v1/commodity";

        public CommodityService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<CommodityItem> AddCommodityItemAsync(CommodityItem commodity, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = ApiUrlBase
            };

            return await _requestProvider.PostAsync(builder.ToString(), commodity, token);
        }

        public async Task DeleteCommodityItemAsync(string id, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            await _requestProvider.DeleteAsync(builder.ToString(), token);
        }

        public async Task<ObservableCollection<CommodityItem>> GetCommoditiesAsync(string token = "", int pageIndex = 0, int pageSize = 2)
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<IEnumerable<CommodityItem>>(uri, token = "");

            if (result != null)
                return result?.ToObservableCollection();

            return new ObservableCollection<CommodityItem>();
        }

        public async Task<CommodityItem> GetCommodityItemAsync(int id, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<CommodityItem>(uri, token);

            return result;
        }

        public async Task<ObservableCollection<CommodityItem>> SearchAsync(string term, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/search?term={term}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<IEnumerable<CommodityItem>>(uri, token);

            if (result != null)
                return result?.ToObservableCollection();

            return new ObservableCollection<CommodityItem>();
        }

        public async Task<CommodityItem> UpdateCommodityItemAsync(CommodityItem commodity, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{commodity.Id}"
            };

            return await _requestProvider.PutAsync(builder.ToString(), commodity, token);
        }

        public async Task DeleteCommodityItemAsync(int id, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            await _requestProvider.DeleteAsync(builder.ToString(), token);
        }
    }
}

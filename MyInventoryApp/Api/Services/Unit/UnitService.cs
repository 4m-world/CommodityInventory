using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Api.Services.RequestProvider;
using MyInventoryApp.Extensions;

namespace MyInventoryApp.Api.Services.Unit
{
    public class UnitService : IUnitService
    {
        readonly IRequestProvider _requestProvider;
        const string ApiUrlBase = "api/v1/unit";

        public UnitService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<Models.Unit> AddUnit(Models.Unit unit, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = ApiUrlBase
            };

            var uri = builder.ToString();

            var result = await _requestProvider.PostAsync(uri, unit, token);

            return result;
        }

        public async Task<Models.Unit> GetUnit(int id, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<Models.Unit>(uri, token);

            return result;
        }

        public async Task<ObservableCollection<Models.Unit>> GetUnitsAsync(string token = "", int pageIndex = 0, int pageSize = 20)
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<IEnumerable<Models.Unit>>(uri, token = "");

            if (result != null)
                return result?.ToObservableCollection();

            return new ObservableCollection<Models.Unit>();
        }

        public async Task<Models.Unit> UpdateUnit(Models.Unit unit, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}{unit.Id}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.PutAsync(uri, unit, token);

            return result;
        }

        public async Task DeleteUnit(int id, string token = "")
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            var uri = builder.ToString();

            await _requestProvider.DeleteAsync(uri, token);
        }
    }
}

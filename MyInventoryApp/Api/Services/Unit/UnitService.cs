using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        public async Task<Models.Unit> AddUnit(Models.Unit unit, string token)
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = ApiUrlBase
            };

            var uri = builder.ToString();

            var result = await _requestProvider.PostAsync(uri, unit, token);

            return result;
        }

        public async Task<Models.Unit> GetUnit(int id, string token)
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}/{id}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<Models.Unit>(uri, token);

            return result;
        }

        public async Task<ObservableCollection<Models.Unit>> GetUnitsAsync(string token)
        {
            var builder = new UriBuilder(GlobalSettings.Instance.UnitEndpoint)
            {
                Path = $"{ApiUrlBase}"
            };

            var uri = builder.ToString();

            var result = await _requestProvider.GetAsync<IEnumerable<Models.Unit>>(uri, token);

            if (result != null)
                return result?.ToObservableCollection();
            
            return new ObservableCollection<Models.Unit>();
        }
    }
}

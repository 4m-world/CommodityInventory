using MyInventoryApp.Services.Dependency;
using MyInventoryApp.Services.Storage;

namespace MyInventoryApp.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        readonly IStorageService _storageService;

        IStorageService AppSettings => _storageService;

        const string AccessToken = "access_token";
        const string IdToken = "id_token";
        const string IdUseMock = "use_mock";
        const string IdBaseUrl = "base_url";

        readonly string AccessTokenDefault = string.Empty;
        readonly string IdTokenDefault = string.Empty;
        readonly bool UseMockDefault = true;
        readonly string BaseUrlDefault = "127.0.0.1";

        public string AuthAccessToken
        {
            get => AppSettings.GetValueOrDefault(AccessToken, AccessTokenDefault);
            set => AppSettings.AddOrUpdateValue(AccessToken, value);
        }

        public string AuthIdToken 
        {
            get => AppSettings.GetValueOrDefault(IdToken, IdTokenDefault);
            set => AppSettings.AddOrUpdateValue(IdToken, value);
        }

        public bool UseMock 
        {
            get => AppSettings.GetValueOrDefault(IdUseMock, UseMockDefault);
            set => AppSettings.AddOrUpdateValue(IdUseMock, value);
        }

        public string BaseUrl 
        {
            get => AppSettings.GetValueOrDefault(IdBaseUrl, BaseUrlDefault);
            set => AppSettings.AddOrUpdateValue(IdBaseUrl, value);
        }

        public SettingsService(IDependencyService dependencyService)
        {
            _storageService = dependencyService.Get<IStorageService>();
        }
    }
}

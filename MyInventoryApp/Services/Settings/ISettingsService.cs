
namespace MyInventoryApp.Services.Settings
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }

        string AuthIdToken { get; set; }

        bool UseMock { get; set; }

        string BaseUrl { get; set; }
    }
}

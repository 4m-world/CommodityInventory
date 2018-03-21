
namespace MyInventoryApp.Services.Storage
{
    public interface IStorageService
    {
        T GetValueOrDefault<T>(string key, T defaultValue);

        void AddOrUpdateValue<T>(string key, T value);

        void Remove(string key);
    }
}

using System.Threading.Tasks;

namespace CoralBayDivingCenter.Interfaces
{
    public interface IStorageService
    {
        // Preferences.
        void PreferencesAddAsync(string key, string value);
        string PreferencesGetAsync(string key);
        void PreferencesRemoveAsync(string key);
        void PreferencesClear();

        // SecureStorage.
        Task SecureStorageAddAsync(string key, string value);
        Task<T> SecureStorageGetAsync<T>(string key, bool isJson = false);
        Task<bool> SecureStorageRemoveAsync(string key);
        void SecureStorageClear();
    }
}

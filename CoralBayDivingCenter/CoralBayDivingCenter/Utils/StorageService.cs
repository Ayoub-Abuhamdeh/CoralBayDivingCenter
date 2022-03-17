using CoralBayDivingCenter.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CoralBayDivingCenter.Utils
{
    public class StorageService : IStorageService
    {
        // Add values to preferences of mobile.
        public void PreferencesAddAsync(string key, string value)
        {
            Preferences.Set(key, value);
        }

        // Clear all data from preferences of mobile.
        public void PreferencesClear()
        {
            Preferences.Clear();
        }

        // Get data from preferences of mobile.
        public string PreferencesGetAsync(string key)
        {
            return Preferences.Get(key, null);
        }

        // Remove specific date from mobile's preferences.
        public void PreferencesRemoveAsync(string key)
        {
            Preferences.Remove(key);
        }

        // Add values to mobile's secure storage.
        public async Task SecureStorageAddAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }

        // Clear all date from mobile's secure storage.
        public void SecureStorageClear()
        {
            SecureStorage.RemoveAll();
        }

        // Get data from mobile's secure storage.
        public async Task<T> SecureStorageGetAsync<T>(string key, bool isJson = false)
        {
            try
            {
                var returnedValue = await SecureStorage.GetAsync(key);
                var returnedValuesUpdated = isJson ? JsonConvert.DeserializeObject<T>(returnedValue) : (T)Convert.ChangeType(returnedValue, typeof(T));
                return returnedValuesUpdated;
            }
            catch (Exception ex)
            {                
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        // Remove specific date from mobile's secure storage.
        public Task<bool> SecureStorageRemoveAsync(string key)
        {
            return Task.FromResult(SecureStorage.Remove(key));
        }
    }
}

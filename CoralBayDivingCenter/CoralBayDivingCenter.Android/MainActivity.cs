using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using CoralBayDivingCenter.Configurations;
using CoralBayDivingCenter.Droid.DependencyServices;
using CoralBayDivingCenter.Droid.GoogleMaps;
using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Models;
using CoralBayDivingCenter.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Plugin.CurrentActivity;
using Xamarin.Forms.GoogleMaps.Android;

namespace CoralBayDivingCenter.Droid
{
    [Activity(Label = "CoralBayDivingCenter", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string NotificationChannelId = "100";
        private const string NotificationChannelName = "Push Notifications";
        private const string NotificationChannelDescription = "Receive notifications";

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //For Bitmap Config
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new BitmapConfig()
            };
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Xamarin.FormsGoogleMapsBindings.Init();

            // Registering native services
            ViewModelLocator.RegisterSingleton<IMessageDisplay, MessageDisplayService>();

            // Check for google play services.
            if (IsPlayServicesAvailable())
            {
                CreateNotificationChannel();
            }

            // Initilize services 
            _ = Startup.Init();

            var _storage = ViewModelLocator.Resolve<IStorageService>();
            var appSettings = await _storage.SecureStorageGetAsync<AppSettings>(AppConstants.AppSettingsKey, true);
            if (appSettings == null)
            {
                var deviceUdid = Android.Provider.Settings.Secure.GetString(Application.Context.ContentResolver, global::Android.Provider.Settings.Secure.AndroidId);
                appSettings = new AppSettings()
                {
                    DeviceSettings = new DeviceSettings()
                    {
                        DeviceUdid = deviceUdid,
                    },
                };
                await _storage.SecureStorageAddAsync(AppConstants.AppSettingsKey, appSettings.ToJson());
            }

            LoadApplication(new App());
        }
        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Check if the app has it's google play services already running.
        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }

        }

        // Create the notification channel to handle send and recieve notification through it.
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);

                var notificationChannel = new NotificationChannel(NotificationChannelId, NotificationChannelName, NotificationImportance.High)
                {
                    Description = NotificationChannelDescription
                };

                notificationManager.CreateNotificationChannel(notificationChannel);
            }
        }
    }
}
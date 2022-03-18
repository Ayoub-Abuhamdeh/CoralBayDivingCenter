using System;
using System.Drawing;
using Android.App;
using Android.Widget;
using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Models;
using Google.Android.Material.Snackbar;
using Xamarin.Essentials;
using Plugin.CurrentActivity;
using AndroidAssembly = Android;


namespace CoralBayDivingCenter.Droid.DependencyServices
{
    public class MessageDisplayService : IMessageDisplay
    {             

        // Show a toast message for the user.
        public void ToastDisplay(string message)
        {
            try
            {
                Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
               // Log the error
            }
        }

        // Show a snackbar message for the user.
        public void SnackBarDisplay(string message, MessageType messageType, int durationSec = 3)
        {
            try
            {
                Activity activity = CrossCurrentActivity.Current.Activity;
                var activityRootView = activity.FindViewById(AndroidAssembly.Resource.Id.Content);
                var snackbar = Snackbar.Make(activityRootView, message, (int)TimeSpan.FromSeconds(durationSec).TotalMilliseconds);
                SetColor(snackbar, messageType);
                snackbar.Show();
            }
            catch (Exception ex)
            {
                // Log the error
            }
        }

        // Set the color of the message depends on the message type.
        private void SetColor(Snackbar snackbar, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Info:
                    snackbar.View.SetBackgroundColor(ColorExtensions.ToPlatformColor(Color.Black));
                    snackbar.SetTextColor(ColorExtensions.ToPlatformColor(Color.White));
                    break;
                case MessageType.Warning:
                    snackbar.View.SetBackgroundColor(ColorExtensions.ToPlatformColor(Color.Orange));
                    snackbar.SetTextColor(ColorExtensions.ToPlatformColor(Color.White));
                    break;
                case MessageType.Error:
                    snackbar.View.SetBackgroundColor(ColorExtensions.ToPlatformColor(Color.Red));
                    snackbar.SetTextColor(ColorExtensions.ToPlatformColor(Color.White));
                    break;
                case MessageType.Success:
                    snackbar.View.SetBackgroundColor(ColorExtensions.ToPlatformColor(Color.Green));
                    snackbar.SetTextColor(ColorExtensions.ToPlatformColor(Color.White));
                    break;
            }
        }
    }
}

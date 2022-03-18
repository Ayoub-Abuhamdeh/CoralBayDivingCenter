using System;
using System.Drawing;
using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Models;
using Foundation;
using UIKit;
using Xamarin.Essentials;
using TTGSnackBar;

namespace CoralBayDivingCenter.iOS.DependencyServices
{
    public class MessageDisplayService : IMessageDisplay
    {
        private const double LONG_DELAY = 3.5;
        private NSTimer alertDelay;
        private UIAlertController alert;

        // Show a toast message for the user.
        public void ToastDisplay(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }

        private void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }

        // Show a snackbar message for the user.
        public void SnackBarDisplay(string message, MessageType messageType, int durationSec)
        {
            var snackbar = new TTGSnackbar(message);
            snackbar.Duration = TimeSpan.FromSeconds(durationSec);
            SetColor(snackbar, messageType);
            snackbar.LocationType = TTGSnackbarLocation.Bottom;
            snackbar.Show();
        }

        // Set the color of the message depends on the message type.
        private void SetColor(TTGSnackbar snackbar, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Info:
                    snackbar.BackgroundColor = ColorExtensions.ToPlatformColor(Color.Black);
                    snackbar.MessageTextColor = ColorExtensions.ToPlatformColor(Color.White);
                    break;
                case MessageType.Warning:
                    snackbar.BackgroundColor = ColorExtensions.ToPlatformColor(Color.Orange);
                    snackbar.MessageTextColor = ColorExtensions.ToPlatformColor(Color.White);
                    break;
                case MessageType.Error:
                    snackbar.BackgroundColor = ColorExtensions.ToPlatformColor(Color.Red);
                    snackbar.MessageTextColor = ColorExtensions.ToPlatformColor(Color.White);
                    break;
                case MessageType.Success:
                    snackbar.BackgroundColor = ColorExtensions.ToPlatformColor(Color.Green);
                    snackbar.MessageTextColor = ColorExtensions.ToPlatformColor(Color.White);
                    break;
            }
        }
    }
}

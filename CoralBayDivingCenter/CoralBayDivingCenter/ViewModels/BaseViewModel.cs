using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Resources;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        private bool isArabic = LanguageDirection.FlowDirection == FlowDirection.RightToLeft;
        public bool IsArabic
        {
            get { return isArabic; }
            set
            {
                isArabic = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool isInternetConnected;
        public bool IsInternetConnected
        {
            get => isInternetConnected;
            set
            {
                isInternetConnected = value;
                OnPropertyChanged();
            }
        }

        public BaseViewModel()
        {
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            IsInternetConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        ~BaseViewModel()
        {
            Connectivity.ConnectivityChanged -= OnConnectivityChanged;
        }

        // Notify user about current netwerk state.
        protected virtual void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsInternetConnected = e.NetworkAccess == NetworkAccess.Internet;
            if (IsInternetConnected)
            {
                //_messageDisplay.SnackBarDisplay("Interent connection is back", MessageType.Success);
            }
            else
            {
                //_messageDisplay.SnackBarDisplay("No Internet connection", MessageType.Error);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

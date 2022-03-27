using CoralBayDivingCenter.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public class OtpVerificationViewModel : BaseViewModel, IPageLifeCycleState
    {
        private readonly INavigationService _navigationService;

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string pinNumber;
        public string PinNumber
        {
            get => pinNumber;
            set
            {
                pinNumber = value;
                OnPropertyChanged();
            }
        }

        public Command VerifyCommand { get; }

        public OtpVerificationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            VerifyCommand = new Command(async () => await VerifyPinNumberAsync(), () => !IsBusy);
        }

        private async Task VerifyPinNumberAsync()
        {
            try
            {
                IsBusy = true;

                Application.Current.MainPage = new AppShell();
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override void ChangeCommandState()
        {

        }

        public Task OnAppearing(object[] parameters = null)
        {
            if (parameters != null)
            {
                PhoneNumber = (string)parameters[0];
            }

            return Task.CompletedTask;
        }

        public Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
    }
}

using CoralBayDivingCenter.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public class SignInViewModel : BaseViewModel
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

        private bool isPhoneNumberValid;
        public bool IsPhoneNumberValid
        {
            get => isPhoneNumberValid;
            set
            {
                isPhoneNumberValid = value;
                OnPropertyChanged();
            }
        }


        public Command RequestOtpCommand { get; }

        public SignInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            RequestOtpCommand = new Command(async () => await RequestOtpAsync(), () => !IsBusy);
        }

        private async Task RequestOtpAsync()
        {
            try
            {
                IsBusy = true;

                object[] parameters = { PhoneNumber };
                await _navigationService.NavigateToAsync<OtpVerificationViewModel>(parameters);
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
            RequestOtpCommand?.ChangeCanExecute();
        }
    }
}

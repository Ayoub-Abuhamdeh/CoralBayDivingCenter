using CoralBayDivingCenter.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        public Command LoginCommand { get; }

        public SignInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
        }

        private async Task LoginAsync()
        {
            try
            {
                IsBusy = true;

                Application.Current.MainPage = new AppShell();
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

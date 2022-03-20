using CoralBayDivingCenter.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public class IntroViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public Command ExploreCommand { get; }

        public IntroViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ExploreCommand = new Command(async () => await ExploreAsync(), () => !IsBusy);
        }

        private async Task ExploreAsync()
        {
            try
            {
                IsBusy = true;

                await _navigationService.NavigateToAsync<SignInViewModel>();
            }
            catch (Exception)
            {
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override void ChangeCommandState()
        {
            ExploreCommand?.ChangeCanExecute();
        }
    }
}

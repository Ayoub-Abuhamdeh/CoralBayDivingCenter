using CoralBayDivingCenter.Configurations;
using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Models;
using CoralBayDivingCenter.ViewModels;
using CoralBayDivingCenter.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoralBayDivingCenter.Utils
{
    public class NavigationService : INavigationService
    {

        private readonly IStorageService _storageService;

        public NavigationService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task InitializeAsync()
        {
            var appSettings = await _storageService.SecureStorageGetAsync<AppSettings>(AppConstants.AppSettingsKey, true);
            if (appSettings != null)
            {
                if ((!appSettings?.IsLoggedIn) ?? true) // Navigate to login view
                {
                    await NavigateToAsync<SignInViewModel>();
                    return;
                }
            }


            // All data is correct and driver is logged in, Then =>

            Application.Current.MainPage = new AppShell();

            return;
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task SetRootPage<TViewModel>() where TViewModel : BaseViewModel
        {
            var page = CreatePage(typeof(TViewModel));
            var root = Application.Current.MainPage.Navigation.NavigationStack[0];
            Application.Current.MainPage.Navigation.InsertPageBefore(page, root);
            return Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public Task NavigateToAsync<TViewModel>(object[] parameters) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameters);
        }

        public Task NavigateToPopupAsync<TViewModel>(object[] parameters) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameters, true);
        }


        private async Task InternalNavigateToAsync(Type viewModelType, object[] parameters, bool isPopup = false)
        {
            var page = CreatePage(viewModelType);
            page.Appearing += (sender, args) => (((BindableObject)sender).BindingContext as IPageLifeCycleState)?.OnAppearing(parameters);
            page.Disappearing += (sender, args) => (((BindableObject)sender).BindingContext as IPageLifeCycleState)?.OnDisappearing();

            if (Application.Current.MainPage is CBDCNavigationView navigationPage)
            {
                if (isPopup)
                {
                    var popupPage = page as PopupPage;
                    await PopupNavigation.Instance.PushAsync(popupPage);

                    return;
                }

                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new CBDCNavigationView(page);
            }
        }

        private Page CreatePage(Type viewModelType)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace(".ViewModels", ".Views");
            viewName = viewName.Replace("ViewModel", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        public async Task GoBackAsync()
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current?.MainPage?.Navigation?.PopAsync();
            });
        }

    }
}

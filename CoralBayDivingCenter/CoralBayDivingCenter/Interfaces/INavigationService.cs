using CoralBayDivingCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoralBayDivingCenter.Interfaces
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task SetRootPage<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object[] parameter) where TViewModel : BaseViewModel;
        Task NavigateToPopupAsync<TViewModel>(object[] parameters) where TViewModel : BaseViewModel;
        Task GoBackAsync();
    }
}

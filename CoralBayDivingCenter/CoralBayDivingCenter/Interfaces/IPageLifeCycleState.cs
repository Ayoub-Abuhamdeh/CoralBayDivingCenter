using System.Threading.Tasks;

namespace CoralBayDivingCenter.Interfaces
{
    public interface IPageLifeCycleState
    {
        Task OnAppearing(object[] parameters = null);
        Task OnDisappearing();
    }
}

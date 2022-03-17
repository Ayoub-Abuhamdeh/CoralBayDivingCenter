
namespace CoralBayDivingCenter.Interfaces
{
    public interface IInternetService
    {
        bool CheckInternetConnectionWithMessage(bool withErrorMsg = true);

        bool CheckInternetConnection();
    }
}

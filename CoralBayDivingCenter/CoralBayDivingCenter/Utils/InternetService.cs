using CoralBayDivingCenter.Interfaces;
using Xamarin.Essentials;

namespace CoralBayDivingCenter.Utils
{
    public class InternetService : IInternetService
    {

        // Check Intenet Connection without diplay message.
        public bool CheckInternetConnection()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        // Check Internet connectivity with display message. 
        public bool CheckInternetConnectionWithMessage(bool withErrorMsg = true)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                if (withErrorMsg)
                {
                    // TODO: Message should be added here
                }
                return false;
            }
            return true;
        }
    }
}

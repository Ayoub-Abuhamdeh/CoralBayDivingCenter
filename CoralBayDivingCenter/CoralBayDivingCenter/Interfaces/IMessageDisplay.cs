using CoralBayDivingCenter.Models;

namespace CoralBayDivingCenter.Interfaces
{
    public interface IMessageDisplay
    {
        void SnackBarDisplay(string message, MessageType messageType, int durationInSeconds = 3);

        void ToastDisplay(string message);
    }
}

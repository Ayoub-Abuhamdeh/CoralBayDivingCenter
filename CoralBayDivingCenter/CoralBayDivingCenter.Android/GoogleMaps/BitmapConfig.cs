using Xamarin.Forms.GoogleMaps.Android.Factories;

namespace CoralBayDivingCenter.Droid.GoogleMaps
{
    public class BitmapConfig : IBitmapDescriptorFactory
    {
        public Android.Gms.Maps.Model.BitmapDescriptor ToNative(Xamarin.Forms.GoogleMaps.BitmapDescriptor descriptor)
        {
            return null;
        }
    }
}
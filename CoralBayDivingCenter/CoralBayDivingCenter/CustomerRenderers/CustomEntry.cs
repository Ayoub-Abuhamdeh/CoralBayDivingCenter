using System;
using Xamarin.Forms;

namespace CoralBayDivingCenter.CustomerRenderers
{
    public class CustomEntry : Entry
    {

        public delegate void BackspaceEventHandler(object sender, EventArgs e);

        public event BackspaceEventHandler OnBackspace;

        public void OnBackspacePressed()
        {
            OnBackspace?.Invoke(null, null);
        }
    }
}

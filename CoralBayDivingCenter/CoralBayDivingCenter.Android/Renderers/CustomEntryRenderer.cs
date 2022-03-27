using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using CoralBayDivingCenter.CustomerRenderers;
using CoralBayDivingCenter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace CoralBayDivingCenter.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                Control.Background = null;

                var lp = new MarginLayoutParams(Control.LayoutParameters);
                LayoutParameters = lp;
                Control.LayoutParameters = lp;
            }
        }

        public override bool DispatchKeyEvent(KeyEvent e)
        {
            if (e.Action == KeyEventActions.Down)
            {
                if (e.KeyCode == Keycode.Del)
                {
                    if (string.IsNullOrWhiteSpace(Control.Text))
                    {
                        var entry = (CustomEntry)Element;
                        entry.OnBackspacePressed();
                    }
                }
            }
            return base.DispatchKeyEvent(e);
        }
    }

}
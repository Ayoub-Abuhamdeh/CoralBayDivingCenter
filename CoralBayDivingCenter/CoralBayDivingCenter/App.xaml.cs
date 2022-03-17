using CoralBayDivingCenter.Interfaces;
using CoralBayDivingCenter.Resources;
using CoralBayDivingCenter.ViewModels;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace CoralBayDivingCenter
{
    public partial class App : Application
    {
        //private readonly IThemeResolver _themeResolver;

        public App()
        {
            InitializeComponent();

            _ = Startup.Init(); // To register all our services and viewmodels into dependency injection
            _ = InitNavigation();
           
            //_themeResolver = ViewModelLocator.Resolve<IThemeResolver>();
            //dictionary.MergedDictionaries.Add(_themeResolver.GetDeciveTheme());

            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
            GetPhoneCluture();
        }


        private Task InitNavigation()
        {
            var navigationservice = ViewModelLocator.Resolve<INavigationService>();
            return navigationservice.InitializeAsync();
        }

        private void GetPhoneCluture()
        {
            // Get the phone's culture.
            var language = CultureInfo.CurrentUICulture.Name;
            var ci = language.Substring(0, 2);
            // Load culture.
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(ci);
            LocalizationResourceManager.Current.CurrentCulture = cultureInfo;
            // Change layout direction. 
            if (ci == "ar")
            {
                LanguageDirection.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                LanguageDirection.FlowDirection = FlowDirection.LeftToRight;
            }
        }
    }
}

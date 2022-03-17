using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace CoralBayDivingCenter.ViewModels
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached(
            "AutoWireViewModel",
            typeof(bool),
            typeof(ViewModelLocator),
            default(bool),
            propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _ = Startup.ServiceCollection
                   .AddSingleton<TInterface, T>()
                   .BuildServiceProvider();
        }

        public static T Resolve<T>() where T : class
        {
            return Startup.ServiceProvider.GetService<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var view = bindable as Element;

                var viewType = view?.GetType();
                if (viewType?.FullName == null)
                {
                    throw new Exception("Check the  namespace for (View and ViewModel)");
                }

                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

                var viewModelType = Type.GetType(viewModelName);
                if (viewModelType == null)
                {
                    throw new Exception("Check the  namespace for (View and ViewModel)");
                }

                var viewModel = Startup.ServiceProvider.GetService(viewModelType);
                if (viewModel == null)
                {
                    throw new Exception("the service dose not register");
                }

                view.BindingContext = viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("the page is not register");
            }
        }
    }
}

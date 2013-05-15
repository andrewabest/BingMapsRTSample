using System;
using System.Collections.Generic;
using Autofac;
using MapsSample.ViewModels;
using Windows.ApplicationModel.Activation;

namespace MapsSample
{
    public sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            IoC.LetThereBeIoC();
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                IoC.Container.TryResolve(service, out instance);
            }
            else
            {
                IoC.Container.TryResolveNamed(key, service, out instance);
            }

            return instance;
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return IoC.Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            IoC.Container.InjectProperties(instance);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootViewFor<MapViewModel>();
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using BingMapMVVM;
using Caliburn.Micro;
using MapsSample.ViewModels;
using MapsSample.Views;
using Windows.UI.Xaml.Controls;

namespace MapsSample
{
    public static class IoC
    {
        public static Frame Frame { get; set; }

        public static IContainer Container { get; private set; }

        public static void LetThereBeIoC(ContainerBuildOptions options = ContainerBuildOptions.None)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(MapViewModel).GetTypeInfo().Assembly)
                   .InNamespaceOf<MapViewModel>()
                   .Where(t => !t.GetTypeInfo().IsAbstract)
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(MapView).GetTypeInfo().Assembly)
                   .Where(t => t.Namespace != null && t.Namespace.Contains("Views"))
                   .Where(t => t.Name.EndsWith("View"))
                   .AsSelf()
                   .InstancePerDependency();

            builder.RegisterType<EventAggregator>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterModule<EventAggregationSubscriptionModule>();
            builder.Register(c => new FrameAdapter(Frame)).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<MapItem>().AsSelf().InstancePerDependency();

            Container = builder.Build(options);
        }
    }

    public class EventAggregationSubscriptionModule : Autofac.Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        static void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            if (e == null)
                return;

            var handler = e.Instance as IHandle;
            if (handler != null)
                e.Context.Resolve<IEventAggregator>().Subscribe(handler);
        }
    }
}

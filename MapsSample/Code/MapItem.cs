using Caliburn.Micro;
using MapsSample.ViewModels;

namespace BingMapMVVM
{
    using Bing.Maps;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public class MapItem : ContentControl
    {
        private readonly IEventAggregator _eventAggregator;

        public MapItem(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public double Latitude
        {
            get { return (double)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }

        public static readonly DependencyProperty LatitudeProperty =
            DependencyProperty.Register("Latitude", typeof(double), typeof(MapItem), new PropertyMetadata(0d, OnLatitudePropertyChanged));

        private static void OnLatitudePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((MapItem)sender).OnLatitudePropertyChanged((double)eventArgs.OldValue, (double)eventArgs.NewValue);
        }

        public double Longitude
        {
            get { return (double)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        }

        public static readonly DependencyProperty LongitudeProperty =
            DependencyProperty.Register("Longitude", typeof(double), typeof(MapItem), new PropertyMetadata(0d, OnLongitudePropertyChanged));

        private static void OnLongitudePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            ((MapItem)sender).OnLongitudePropertyChanged((double)eventArgs.OldValue, (double)eventArgs.NewValue);
        }

        private void OnLatitudePropertyChanged(double oldValue, double newValue)
        {
            UpdatePosition();
        }

        private void OnLongitudePropertyChanged(double oldValue, double newValue)
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var location = new Location(Latitude, Longitude);
            MapLayer.SetPosition(this, location);
        }

        protected override void OnTapped(Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var location = DataContext as LocationModel;

            if (location != null)
            {
                _eventAggregator.Publish(new LocationSelectedMessage(location));
            }
        }

        public static T FindParent<T>(DependencyObject parent)
            where T : DependencyObject
        {
            var result = default(T);
            do
            {
                var p = VisualTreeHelper.GetParent(parent);
                if (p == null)
                    return result;

                result = p as T;
                if (result != null)
                    return result;

                parent = p;

            } while (result == null);
            return null;
        }
        
    }
}

using Windows.UI.Xaml;

namespace BingMapMVVM
{
    public class LocationAwareDataTemplate
    {
        public static string GetLongitudePath(DependencyObject obj)
        {
            return (string)obj.GetValue(LongitudePathProperty);
        }

        public static void SetLongitudePath(DependencyObject obj, string value)
        {
            obj.SetValue(LongitudePathProperty, value);
        }

        public static readonly DependencyProperty LongitudePathProperty =
            DependencyProperty.RegisterAttached("LongitudePath", typeof(string), typeof(DataTemplate), new PropertyMetadata(0));

        public static string GetLatitudePath(DependencyObject obj)
        {
            return (string)obj.GetValue(LatitudePathProperty);
        }

        public static void SetLatitudePath(DependencyObject obj, string value)
        {
            obj.SetValue(LatitudePathProperty, value);
        }

        public static readonly DependencyProperty LatitudePathProperty =
            DependencyProperty.RegisterAttached("LatitudePath", typeof(string), typeof(DataTemplate), new PropertyMetadata(0));
    }
}

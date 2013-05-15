using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MapsSample.Code
{
    public class MapItemDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return (DataTemplate)Application.Current.Resources["BasicTemplate"];
        }
    }
}

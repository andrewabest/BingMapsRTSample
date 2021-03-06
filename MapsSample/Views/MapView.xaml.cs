﻿using Caliburn.Micro;
using MapsSample.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapsSample.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapView : Page, IHandle<CentreMapRequestMessage>
    {
        public MapView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void Handle(CentreMapRequestMessage message)
        {
            var mapLocation = new Bing.Maps.Location(message.Location.Latitude, message.Location.Longitude);
            
            MapControl.SetView(mapLocation);
        }
    }
}

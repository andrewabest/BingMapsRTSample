using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BingMapMVVM;
using Caliburn.Micro;
using Windows.UI.Popups;

namespace MapsSample.ViewModels
{
    public class MapViewModel : Screen, IHandle<LocationSelectedMessage>
    {
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<LocationModel> _locations;
        private ObservableCollection<LocationModel> _mapLocations;
        private LocationModel _selectedLocation;
        private string _searchText;

        public MapViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AllLocations = new Collection<LocationModel>();
            _locations = new ObservableCollection<LocationModel>();
            _mapLocations = new ObservableCollection<LocationModel>();
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value; 
                NotifyOfPropertyChange(); 
                FilterLocations();
            }
        }

        public LocationModel SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value; 
                NotifyOfPropertyChange();
            }
        }

        public ICollection<LocationModel> AllLocations { get; private set; } 

        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                NotifyOfPropertyChange();
            }
        }

        public ObservableCollection<LocationModel> MapLocations
        {
            get { return _mapLocations; }
            set
            {
                _mapLocations = value; 
                NotifyOfPropertyChange();
            }
        }

        protected override void OnInitialize()
        {
            AllLocations.AddRange(GetFootballGrounds());
            MapLocations.AddRange(AllLocations);
            Locations.AddRange(AllLocations);
        }

        private void FilterLocations()
        {
            if (!AllLocations.Any()) return;

            var locations =
                string.IsNullOrWhiteSpace(SearchText)
                    ? AllLocations
                    : AllLocations.Where(a => a.Name.Contains(SearchText.ToUpperInvariant()));

            Locations.Clear();
            Locations.AddRange(locations);

            MapLocations.Clear();
            MapLocations.AddRange(locations);

            if (MapLocations.Count == 1)
            {
                var location = MapLocations.First();

                _eventAggregator.Publish(new CentreMapRequestMessage(location));
            }
        }

        private static IEnumerable<LocationModel> GetFootballGrounds()
        {
            yield return
                new LocationModel() {Name = "Melbourne Cricket Ground", Latitude = -37.819751, Longitude = 144.981658};
            yield return new LocationModel()
                             {
                                 Name = "ANZ Stadium",
                                 Latitude = -33.847358,
                                 Longitude = 151.063256
                             };
            yield return new LocationModel() { Name = "Docklands Stadium", Latitude = -37.816404, Longitude = 144.947745 };
            yield return new LocationModel()
            {
                Name = "AAMI Stadium",
                Latitude = -34.881680, 
                Longitude = 138.495884
            };
            yield return new LocationModel() { Name = "Sydney Cricket Ground", Latitude = -33.891641, Longitude = 151.225004 };
            yield return new LocationModel()
            {
                Name = "Subiaco Oval",
                Latitude = -31.944579,
                Longitude = 115.830145
            };
            yield return new LocationModel() { Name = "The Gabba", Latitude = -27.485916, Longitude = 153.038231 };
        }

        public async void Handle(LocationSelectedMessage message)
        {
            var dialog = new MessageDialog(message.Location.Name, "Location Selected");

            await dialog.ShowAsync();
        }
    }

    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}

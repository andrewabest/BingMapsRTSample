using MapsSample.ViewModels;

namespace BingMapMVVM
{
    public class LocationSelectedMessage
    {
        public LocationModel Location { get; set; }

        public LocationSelectedMessage(LocationModel location)
        {
            Location = location;
        }
    }
}
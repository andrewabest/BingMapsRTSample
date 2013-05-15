namespace MapsSample.ViewModels
{
    public class CentreMapRequestMessage
    {
        public LocationModel Location { get; set; }

        public CentreMapRequestMessage(LocationModel location)
        {
            Location = location;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsSample.ViewModels
{
    public class LocationModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }

        public string Description
        {
            get { return "The world's most awesome footy ground!"; }
        }
    }
}

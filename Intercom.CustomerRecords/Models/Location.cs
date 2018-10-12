using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Models
{
    public class Location
    {
        public Location(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double GetLatitudeInRadians()
        {
            return (Math.PI / 180) * this.Latitude;
        }

        public double GetLongitudeInRadians()
        {
            return (Math.PI / 180) * this.Longitude;
        }
    }
}

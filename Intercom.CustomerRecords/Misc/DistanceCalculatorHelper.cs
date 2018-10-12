using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Misc
{
    public static class DistanceCalculatorHelper
    {
        public static double CalculateDistance(Location locationA, Location locationB)
        {
            return HarvesineDistance(locationA.Latitude, locationA.Longitude, locationB.Latitude, locationB.Longitude);
        }

        private static double HarvesineDistance(double latA, double longA, double latB, double longB)
        {
            double radiusInKm = 6371;
            var dLat = GetRadians(latB - latA);
            var dLong = GetRadians(longB - longA);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                          Math.Cos(GetRadians(latA)) * Math.Cos(GetRadians(latB)) *
                          Math.Sin(dLong / 2) * Math.Sin(dLong / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));

            return radiusInKm * c;
        }

        public static double GetRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
    }
}

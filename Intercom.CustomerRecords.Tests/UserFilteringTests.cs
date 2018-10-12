using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services.Filters;
using Intercom.CustomerRecords.Services.Filters.CustomerFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Intercom.CustomerRecords.Tests
{
    public class UserFilteringTests
    {
        [Fact]
        public void DistanceCriteria_ReturnsExpectedResults()
        {
            IList<User> initialList = new List<User>();

            User userA = new User(1, "Jose");
            User userB = new User(1, "Nataly");
            User userC = new User(1, "Connor");

            Location locationA = new Location(9.923818, -84.251773);
            Location locationB = new Location(52.986375, -6.043701);
            Location locationC = new Location(54.133333, -6.433333);

            userA.Location = locationA;
            userB.Location = locationB;
            userC.Location = locationC;

            initialList.Add(userA);
            initialList.Add(userB);
            initialList.Add(userC);

            int distanceInKilometers = 100;
            Location headquatersLocation = new Location(53.339428, -6.257664);

            IFilteringCriteria<User> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<User> result = distanceCriteria.DoSearch(initialList);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(-1, result.IndexOf(userA));

        }
        
        
    }
}

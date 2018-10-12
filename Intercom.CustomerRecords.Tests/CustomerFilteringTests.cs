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
    public class CustomerFilteringTests
    {
        [Fact]
        public void DistanceCriteria_ReturnsExpectedResults()
        {
            IList<Customer> initialList = new List<Customer>();

            Customer customerA = new Customer(1, "Jose");
            Customer customerB = new Customer(1, "Nataly");
            Customer customerC = new Customer(1, "Connor");

            Location locationA = new Location(9.923818, -84.251773);
            Location locationB = new Location(52.986375, -6.043701);
            Location locationC = new Location(54.133333, -6.433333);

            customerA.Location = locationA;
            customerB.Location = locationB;
            customerC.Location = locationC;

            initialList.Add(customerA);
            initialList.Add(customerB);
            initialList.Add(customerC);

            int distanceInKilometers = 100;
            Location headquatersLocation = new Location(53.339428, -6.257664);

            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<Customer> result = distanceCriteria.DoSearch(initialList);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(-1, result.IndexOf(customerA));

        }
        
        
    }
}

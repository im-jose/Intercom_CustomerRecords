using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services.Filters;
using Intercom.CustomerRecords.Services.Filters.CustomerFilters;
using System.Collections.Generic;
using Xunit;

namespace Intercom.CustomerRecords.Tests
{
    public class CustomerFilteringTests
    {
        int distanceInKilometers;
        Location headquatersLocation;

        public CustomerFilteringTests()
        {
            distanceInKilometers = 10;
            headquatersLocation = new Location(53.339428, -6.257664);
        }

        [Fact]
        public void DistanceCriteria_CustomersMeetCriteria_ReturnsExpectedResults()
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

            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<Customer> result = distanceCriteria.DoSearch(initialList);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(-1, result.IndexOf(customerA));
        }

        [Fact]
        public void DistanceCriteria_CustomersNotMeetCriteria_ReturnsEmptyResults()
        {
            IList<Customer> initialList = new List<Customer>();

            Customer customerA = new Customer(1, "Jose");
            Customer customerB = new Customer(1, "Nataly");
            Customer customerC = new Customer(1, "Connor");

            Location locationA = new Location(9.923818, -84.251773);
            Location locationB = new Location(9.547818, -86.043701);
            Location locationC = new Location(9.133333, -96.433333);

            customerA.Location = locationA;
            customerB.Location = locationB;
            customerC.Location = locationC;

            initialList.Add(customerA);
            initialList.Add(customerB);
            initialList.Add(customerC);

            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<Customer> result = distanceCriteria.DoSearch(initialList);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void DistanceCriteria_EmptyCustomersList_ReturnsEmptyResults()
        {
            IList<Customer> initialList = new List<Customer>();

            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<Customer> result = distanceCriteria.DoSearch(initialList);

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void DistanceCriteria_NullCustomersList_ReturnsEmptyResults()
        {
            IList<Customer> initialList = null;
            
            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            IList<Customer> result = distanceCriteria.DoSearch(initialList);

            Assert.Null(result);
        }


    }
}

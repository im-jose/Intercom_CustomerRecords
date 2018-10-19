using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services;
using System;
using System.Configuration;
using System.IO;
using Xunit;

namespace Intercom.CustomerRecords.Tests
{
    public class CustomerFilteringIntegrationTests
    {
        [Fact]
        public void FindCustomersByDistance_ReturnsExpectedResults()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"TestData\", "testData.txt");
            ConfigurationManager.AppSettings["CustomerFile"] = filePath;

            int distanceInKilometers = 100;
            Location headquatersLocation = new Location(53.339428, -6.257664);

            ICustomerJsonParser parser = new CustomerJsonParser();
            ICustomerProvider provider = new CustomerFileReader(parser);
            ICustomerService service = new CustomerService(provider);
            var result = service.FindCustomersByDistance(headquatersLocation, distanceInKilometers);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            for (int i = 1; i < result.Count; i++)
            {
                Assert.True(result[i - 1].Id < result[i].Id);
            }

            Assert.Equal(4, result[0].Id);
            Assert.Equal(5, result[1].Id);
            Assert.Equal(12, result[2].Id);

        }
    }
}

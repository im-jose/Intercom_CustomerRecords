using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Intercom.CustomerRecords.Tests
{
    public class CustomerProviderTest
    {
        [Fact]
        public void GetAllCustomer_ReturnsExpectedResults()
        {
            var mockJsonParser = new Mock<ICustomerJsonParser>();
            mockJsonParser.Setup(x => x.ParseJsonLine(It.IsAny<string>())).Returns(() => new Customer(1, "John Doe"));

            ICustomerProvider customerProvider = new CustomerFileReader(mockJsonParser.Object);
            var result = customerProvider.GetAllCustomers();

            Assert.NotNull(result);
            Assert.Equal(32, result.Count);
        }

        [Fact]
        public void ParseJsonCustomer_ReturnsExpectedModelObject()
        {
            string json = "{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}";
            ICustomerJsonParser parser = new CustomerJsonParser();
            Customer result = parser.ParseJsonLine(json);

            Assert.NotNull(result);
            Assert.Equal("Christina McArdle", result.Name);
            Assert.Equal(12, result.Id);
            Assert.Equal(52.986375, result.Location.Latitude);
            Assert.Equal(-6.043701, result.Location.Longitude);
        }

    }
}

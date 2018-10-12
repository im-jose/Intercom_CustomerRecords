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
    public class UserProviderTest
    {
        [Fact]
        public void GetAllUsers_ReturnsExpectedResults()
        {
            var mockJsonParser = new Mock<IUserJsonParser>();
            mockJsonParser.Setup(x => x.ParseJsonLine(It.IsAny<string>())).Returns(() => new User(1, "John Doe"));

            IUserProvider userProvider = new UserFileReader(mockJsonParser.Object);
            var result = userProvider.getAllUsers();

            Assert.NotNull(result);
            Assert.Equal(32, result.Count);
        }

        [Fact]
        public void ParseJsonCustomer_ReturnsExpectedModelObject()
        {
            string json = "{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}";
            IUserJsonParser parser = new UserJsonParser();
            User result = parser.ParseJsonLine(json);

            Assert.NotNull(result);
            Assert.Equal("Christina McArdle", result.Name);
            Assert.Equal(12, result.Id);
            Assert.Equal(52.986375, result.Location.Latitude);
            Assert.Equal(-6.043701, result.Location.Longitude);
        }

    }
}

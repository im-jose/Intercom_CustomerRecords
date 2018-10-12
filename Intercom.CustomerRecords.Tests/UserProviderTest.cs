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
        public void GetAllUsers_RetursExpectedResults()
        {
            var mockJsonParser = new Mock<IUserJsonParser>();
            mockJsonParser.Setup(x => x.ParseJsonLine(It.IsAny<string>())).Returns(() => new User(1, "JohnDoe"));

            IUserProvider userProvider = new UserFileReader(mockJsonParser.Object);
            var result = userProvider.getAllUsers();

            Assert.NotNull(result);
            Assert.Equal(32, result.Count);
        }

        //test userJsonParser

        //test distanceCalculator

        //test userFiltering
    }
}

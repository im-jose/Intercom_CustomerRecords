using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using Moq;
using System;
using System.Configuration;
using System.IO;
using Xunit;

namespace Intercom.CustomerRecords.Tests
{
    public class CustomerProviderTest
    {
        public CustomerProviderTest()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"TestData\", "testData.txt");
            ConfigurationManager.AppSettings["CustomerFile"] = filePath;
        }

        [Fact]
        public void GetAllCustomer_ReturnsNumberOfExpectedResults()
        {
            var mockJsonParser = new Mock<ICustomerJsonParser>();
            mockJsonParser.Setup(x => x.ParseJsonLine(It.IsAny<string>())).Returns(() => new Customer(1, "John Doe"));

            ICustomerProvider customerProvider = new CustomerFileReader(mockJsonParser.Object);
            var result = customerProvider.GetAllCustomers();

            Assert.NotNull(result);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public void GetAllCustomer_ReturnsSortedResults()
        {
            ICustomerProvider customerProvider = new CustomerFileReader(new CustomerJsonParser());
            var result = customerProvider.GetAllCustomers();

            Assert.NotNull(result);
            for(int i = 1; i < result.Count; i++)
            {
                Assert.True(result[i-1].Id < result[i].Id);
            }
        }

        [Fact]
        public void GetAllCustomer_CanReadExistingFileProvidedInAppSettings()
        {
            ICustomerProvider customerProvider = new CustomerFileReader(new CustomerJsonParser());
            var result = customerProvider.GetAllCustomers();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllCustomer_ReadNonExistingFileProvidedInAppSettings_ThrowsFileNotFoundException()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "NotExistingFile.txt");
            ConfigurationManager.AppSettings["CustomerFile"] = filePath;

            ICustomerProvider customerProvider = new CustomerFileReader(new CustomerJsonParser());

            Assert.Throws<FileNotFoundException>(() => customerProvider.GetAllCustomers());
        }

        [Fact]
        public void ParseJsonCustomer_ExpectedJsonFormat_ReturnsExpectedModelObject()
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

        [Fact]
        public void ParseJsonCustomer_MalformedJson_ReturnsNullObject()
        {
            string json = "[\"user_id\": 12, \"name\": \"Christina 'BadLuck' McArdle\", \"longitude\": \"-6.043701\"}";
            ICustomerJsonParser parser = new CustomerJsonParser();
            Customer result = parser.ParseJsonLine(json);

            Assert.Null(result);
        }

    }
}

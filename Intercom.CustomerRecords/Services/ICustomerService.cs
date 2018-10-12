using System.Collections.Generic;
using Intercom.CustomerRecords.Models;

namespace Intercom.CustomerRecords.Services
{
    public interface ICustomerService
    {
        IList<Customer> GetAllCustomers();
        IList<Customer> FindCustomersByDistance(Location headquatersLocation, int distanceInKilometers);
    }
}
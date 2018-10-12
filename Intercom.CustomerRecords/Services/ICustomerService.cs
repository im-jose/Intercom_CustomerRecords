using System.Collections.Generic;
using Intercom.CustomerRecords.Models;

namespace Intercom.CustomerRecords.Services
{
    public interface ICustomerService
    {
        IList<User> GetAllUsers();
        IList<User> FindUsersByDistance(Location headquatersLocation, int distanceInKilometers);
    }
}
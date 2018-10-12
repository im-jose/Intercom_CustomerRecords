using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services.Filters;
using Intercom.CustomerRecords.Services.Filters.CustomerFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Services
{
    public class CustomerService : ICustomerService
    {
        IList<User> customerList;
        IUserProvider userProvider;

        public CustomerService(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        public IList<User> GetAllUsers()
        {
            if(null == this.customerList)
            {
                this.customerList = this.userProvider.getAllUsers();
            }

            return this.customerList;
        }

        public IList<User> FindUsersByDistance(Location headquatersLocation, int distanceInKilometers)
        {
            IFilteringCriteria<User> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            return distanceCriteria.DoSearch(GetAllUsers());
        }
    }
}

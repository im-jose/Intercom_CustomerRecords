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
        IList<Customer> customerList;
        ICustomerProvider customerProvider;

        public CustomerService(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        public IList<Customer> GetAllCustomers()
        {
            if(null == this.customerList)
            {
                this.customerList = this.customerProvider.GetAllCustomers();
            }

            return this.customerList;
        }

        public IList<Customer> FindCustomersByDistance(Location headquatersLocation, int distanceInKilometers)
        {
            IFilteringCriteria<Customer> distanceCriteria = new DistanceCriteria(headquatersLocation, distanceInKilometers);
            return distanceCriteria.DoSearch(GetAllCustomers());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services;

namespace Intercom.CustomerRecords.Controller
{
    public class CustomerController
    {
        ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        internal IList<User> getUsersByDistance(Location headquatersLocation, int distanceInKilometers)
        {
            return this.customerService.FindUsersByDistance(headquatersLocation, distanceInKilometers);
        }
    }
}

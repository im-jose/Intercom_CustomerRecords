using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Services.Filters.CustomerFilters
{
    public class DistanceCriteria : IFilteringCriteria<Customer>
    {
        int targetDistanceInKm;
        Location headquatersLocation;

        public DistanceCriteria(Location headquatersLocation, int targetDistanceInKm = 100)
        {
            this.headquatersLocation = headquatersLocation;
            this.targetDistanceInKm = targetDistanceInKm;
        }

        public IList<Customer> DoSearch(IList<Customer> initialList)
        {
            if (null == initialList || initialList.Count == 0)
            {
                return initialList;
            }

            IList<Customer> filteredList = new List<Customer>();
            foreach(Customer customer in initialList)
            {
                double distance = DistanceCalculatorHelper.CalculateDistance(headquatersLocation, customer.Location);
                if(distance <= targetDistanceInKm)
                {
                    filteredList.Add(customer);
                }
            }

            return filteredList;
        }
    }
}

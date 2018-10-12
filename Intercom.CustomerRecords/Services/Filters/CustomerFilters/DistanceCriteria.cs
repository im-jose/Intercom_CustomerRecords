using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Services.Filters.CustomerFilters
{
    public class DistanceCriteria : IFilteringCriteria<User>
    {
        int targetDistanceInKm;
        Location headquatersLocation;

        public DistanceCriteria(Location headquatersLocation, int targetDistanceInKm = 100)
        {
            this.headquatersLocation = headquatersLocation;
            this.targetDistanceInKm = targetDistanceInKm;
        }

        public IList<User> DoSearch(IList<User> initialList)
        {
            if (null == initialList || initialList.Count == 0)
            {
                return initialList;
            }

            IList<User> filteredList = new List<User>();
            foreach(User user in initialList)
            {
                double distance = DistanceCalculatorHelper.CalculateDistance(headquatersLocation, user.Location);
                if(distance <= targetDistanceInKm)
                {
                    filteredList.Add(user);
                }
            }

            return filteredList;
        }
    }
}

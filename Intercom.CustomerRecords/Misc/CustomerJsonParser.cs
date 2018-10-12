using Intercom.CustomerRecords.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords.Misc
{
    public class CustomerJsonParser : ICustomerJsonParser
    {
        public Customer ParseJsonLine(string jsonText)
        {
            CustomerDTO dto = null;

            try
            {
                dto = JsonConvert.DeserializeObject<CustomerDTO>(jsonText);
                return GetCustomerModel(dto);
            }
            catch (Exception)
            {
                //TODO: Log Exception
                //swallow exception on purpose
                return null;
            }
        }

        private static Customer GetCustomerModel(CustomerDTO dto)
        {
            Location location = new Location(latitude: dto.latitude, longitude: dto.longitude);
            Customer customer = new Customer(dto.user_id, dto.name);
            customer.Location = location;
            return customer;
        }
    }
}

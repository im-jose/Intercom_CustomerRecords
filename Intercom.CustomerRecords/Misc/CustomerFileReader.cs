using Intercom.CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Intercom.CustomerRecords.Misc
{
    public class CustomerFileReader : ICustomerProvider
    {
        private const string FILENAME = "customers.txt";
        private readonly ICustomerJsonParser customerParser;

        public CustomerFileReader(ICustomerJsonParser customerParser)
        {
            this.customerParser = customerParser;
        }
        
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string[] lines = ReadCustomersFile();

            if (lines != null && lines.Count() > 0)
            {
                foreach (string line in lines)
                {
                    Customer customer = customerParser.ParseJsonLine(line);
                    if(null != customer)
                    {
                        customers.Add(customer);
                    }
                }
            }

            List<Customer> sortedCustomerList = customers.OrderBy(x => x.Id).ToList();
            return sortedCustomerList;
        }

        private static string[] ReadCustomersFile()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\", FILENAME);

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CustomerFile"]))
            {
                filePath = ConfigurationManager.AppSettings["CustomerFile"];
            }

            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException("Customer file can't be found, please check the path provided", filePath);
            }

            return File.ReadAllLines(filePath);
        }
    }
}

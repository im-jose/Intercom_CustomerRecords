using Intercom.CustomerRecords.Controller;
using Intercom.CustomerRecords.Misc;
using Intercom.CustomerRecords.Models;
using Intercom.CustomerRecords.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercom.CustomerRecords
{
    class Program
    {
        static readonly Container container;
        static Program()
        {
            //Initialize DI container & register dependencies
            container = new Container();
            container.Register<ICustomerJsonParser, CustomerJsonParser>();
            container.Register<ICustomerProvider, CustomerFileReader>();
            container.Register<ICustomerService, CustomerService>();
            container.Register<CustomerController>(() => new CustomerController(container.GetInstance<ICustomerService>()));
        }

        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;

            //Initial params - might be read from console as well.
            int distanceInKilometers = 100;
            double headquatersLatitude = 53.339428;
            double headquatersLongitude = -6.257664;

            StartCustomerFiltering(headquatersLatitude, headquatersLongitude, distanceInKilometers);

            Console.WriteLine("End of Program - Press Enter to Exit");
            Console.ReadLine();
        }

        private static void StartCustomerFiltering(double headquatersLatitude, double headquatersLongitude, int distanceInKilometers)
        {
            IList<Customer> customers = FilterCustomerBase(headquatersLatitude, headquatersLongitude, distanceInKilometers);

            PresentCustomerData(customers);
        }

        private static IList<Customer> FilterCustomerBase(double headquatersLatitude, double headquatersLongitude, int distanceInKilometers)
        {
            Location headquatersLocation = new Location(headquatersLatitude, headquatersLongitude);

            CustomerController controller = container.GetInstance<CustomerController>();
            IList<Customer> customers = controller.GetCustomersByDistance(headquatersLocation, distanceInKilometers);

            return customers;
        }

        private static void PresentCustomerData(IList<Customer> customers)
        {
            if (customers == null || customers.Count == 0)
            {
                Console.WriteLine("Couldn't find any customers that met the search criteria");
                return;
            }

            foreach (Customer customer in customers)
            {
                Console.WriteLine(string.Format("Id: {0}, Name: {1}", customer.Id, customer.Name));
            }
        }

        private static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("An un expected excepion ocurred:");
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("This application will close - Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}

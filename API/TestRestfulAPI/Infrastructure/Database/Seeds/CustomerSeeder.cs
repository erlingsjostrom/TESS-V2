using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;

namespace TestRestfulAPI.Infrastructure.Database.Seeds
{
    public class CustomerSeeder
    {
        static int run(string[] args)
        {
            using (var context = new TESSEntities())
            {
                CreateCustomer(context);
            }
            return 0;
        }

        private static void CreateCustomer(TESSEntities context)
        {
            var customer = new Customer();
            customer.Name = "Ny kommun";
            customer.Type = "Kommun";
            customer.WebAddress = "www.nykommun.webaddress";
            customer.CorporateIdentityNumber = "1122334455";
            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine("[" + customer.UpdatedAt.ToShortDateString() + "] >> Customer " + customer.Name + " was created with ID " + customer.Id);
            Console.ReadKey();
        }
    }
}
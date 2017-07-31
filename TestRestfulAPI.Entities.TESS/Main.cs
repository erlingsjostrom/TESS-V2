using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
{
    public class MainClass
    {
        static int Main(string[] args)
        {
            using (var context = new TESSEntities())
            {
                CreateArticle(context);
            }
            return 0;
        }

        private static void CreateArticle(TESSEntities context)
        {
            var article = new Article();
            article.ArticleNumber = 1003;
            article.Name = "Färdtjänst deluxe";
            article.Description = "Privatjet";
            context.Articles.Add(article);
            context.SaveChanges();

            Console.WriteLine("[" + article.UpdatedAt.ToShortDateString() + "] >> Article " + article.Name + " was created with ID " + article.Id);
            Console.ReadKey();
        }

        private static void CreateCustomer(TESSEntities context)
        {
            var customer = new Customer();
            customer.Name = "Ny kommun";
            customer.CustomerType = "Kommun";
            customer.WebAddress = "www.nykommun.webaddress";
            customer.CorporateIdentityNumber = "1122334455";
            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine("[" + customer.UpdatedAt.ToShortDateString() + "] >> Customer " + customer.Name + " was created with ID " + customer.Id);
            Console.ReadKey();
        }
    }
}

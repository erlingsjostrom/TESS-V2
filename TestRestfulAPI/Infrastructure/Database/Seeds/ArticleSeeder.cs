using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;

namespace TestRestfulAPI.Infrastructure.Database.Seeds
{
    public class ArticleSeeder
    {
        static int run(string[] args)
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

    }
}
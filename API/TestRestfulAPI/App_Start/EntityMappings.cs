using System.Web.Http;
using AutoMapper;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Customers.Entities;

namespace TestRestfulAPI
{
    public class EntityMappings
    {
        public static void Register(HttpConfiguration config)
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Article, ArticleDto>();
                    cfg.CreateMap<Customer, CustomerDto>();
                }  
            );
        }
    }
}
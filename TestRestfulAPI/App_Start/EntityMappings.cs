using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using TestRestfulAPI.Entities.TESS;

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
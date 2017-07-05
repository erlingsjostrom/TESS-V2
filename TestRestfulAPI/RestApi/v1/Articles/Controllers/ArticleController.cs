﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Authorization;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Database;
using TestRestfulAPI.Infrastructure.Helpers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.RestApi.v1.Articles.Controllers
{

    [RoutePrefix("api/v1")]
    public class ArticleController : ApiJsonController
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleController()
        {
            this._articleRepository = new ArticleRepository(
                new List<ResourceContext>()
                {
                    new ResourceContext("VOO", DbContextFactory.Get<TESSEntities>("TEST_TESS_DB1"), typeof(TESSEntities)),
                    new ResourceContext("IFO", DbContextFactory.Get<TESSEntities>("TEST_TESS_DB2"), typeof(TESSEntities)),
                }    
            );
        }

        [UserHasRole("ProductOwner")]
        [HttpGet, Route("{articles}")]
        public IHttpActionResult Articles()
        {
           return Json(this._articleRepository.AllWithResourceContext(), "Collection");
        }

        [UserHasPermission("Read")]
        [HttpGet, Route("{resource}/articles")]
        public IHttpActionResult Articles(string resource)
        {
            return Json(this._articleRepository.All(resource), "Articles");
        }

        [HttpGet, Route("{resource}/articles/{id}")]
        public IHttpActionResult Articles(int id, string resource)
        {
            return Json(this._articleRepository.GetWithResourceContext(id, resource), "Article");
        }
    }

    
}
  
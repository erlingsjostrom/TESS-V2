using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Entities.User;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Repositories;
using TestRestfulAPI.RestApi.v1.Articles.Repositories;
using TestRestfulAPI.RestApi.v1.Users.Repositories;

namespace TestRestfulAPI.RestApi.v1.Articles.Controllers
{
    [RoutePrefix("api/v1/articles")]
    public class ArticleController : ApiJsonController
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleController()
        {
            this._articleRepository = new ArticleRepository(
                new List<ResourceContext>()
                {
                    new ResourceContext("VOO", new TESSEntities()),
                    new ResourceContext("IFO", new TESSEntities()),
                    new ResourceContext("EDU", new TESSEntities())
                }    
            );
        }

        [HttpGet, Route("")]
        public IHttpActionResult Articles()
        {
            return Json(this._articleRepository.AllWithResourceContext(), "Articles");
        }
        [HttpGet, Route("{id}/{resource}")]
        public IHttpActionResult Articles(int id, string resource)
        {
            return Json(this._articleRepository.GetWithResourceContext(id, resource), "Article");
        }
        [HttpPost, Route("")]
        public IHttpActionResult Articles(Article article, string resource)
        {
            var result = this._articleRepository.Create(article, resource);
            return JsonCreated(result, result.Article_number);
        }
    }

    
}
  
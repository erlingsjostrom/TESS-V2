﻿using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.Articles.Services;
using TestRestfulAPI.RestApi.odata.Controllers;

namespace TestRestfulAPI.RestApi.odata.Articles.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Articles")]
    public class ArticlesController : ResourceODataController, ICrudController<Article>
    {
        private readonly ArticleService _articleService = GlobalServices.ArticleService;

        // GET: {resource}/Article()
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Article> Get()
        {
            this.ParseResource();
            return this._articleService.All(this.Resource);
        }

        // GET: {resource}/Article({id})
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Article Get(int id)
        {
            this.ParseResource();
            return this._articleService.Get(this.Resource, id);
        }

        // POST: {resource}/Article()
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Article article)
        {
            this.ParseResource();
            return ODataCreated(this._articleService.Create(this.Resource, article), article.Id);
        }

        // PUT: {resource}/Article({id})
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Article Update(int id, [FromBody] Article article)
        {
            this.ParseResource();
            return this._articleService.Update(this.Resource, article);
        }

        // PATCH: {resource}/Article({id})
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Article PartialUpdate(int id, [FromBody] Delta<Article> article)
        {
            this.ParseResource();
            return this._articleService.PartialUpdate(this.Resource, id, article);
        }

        // DELETE: {resource}/Article({id})
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Deleted(int id)
        {
            this.ParseResource();
            this._articleService.Delete(this.Resource, id);
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NoContent;
        }

    }


}

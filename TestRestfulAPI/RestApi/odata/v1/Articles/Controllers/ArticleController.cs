using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.RestApi.odata.v1.Articles.Entities;
using TestRestfulAPI.RestApi.odata.v1.Articles.Services;

namespace TestRestfulAPI.RestApi.odata.v1.Articles.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Articles")]
    [UserHasPermission("ArticleAccess")]
    public class ArticlesController : ResourceODataController, ICrudController<Article>
    {
        private readonly ArticleService _articleService = GlobalServices.ArticleService;

        // GET: {resource}/Articles()
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Article> Get()
        {
            this.ParseResource();
            return this._articleService.All(this.Resource);
        }

        // GET: {resource}/Articles({id})
        [UserHasResourceAccess, UserHasPermission("Read")]
        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Article Get(int id)
        {
            this.ParseResource();
            return this._articleService.Get(this.Resource, id);
        }

        // POST: {resource}/Articles()
        [UserHasResourceAccess, UserHasPermission("Write")]
        [EnableQuery, HttpPost, ODataRoute("()")]
        public IHttpActionResult Create([FromBody] Article article)
        {
            this.ParseResource();
            return ODataCreated(this._articleService.Create(this.Resource, article), article.Id);
        }

        // PUT: {resource}/Articles({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPut, ODataRoute("({id})")]
        public Article Update(int id, [FromBody] Article article)
        {
            this.ParseResource();
            return this._articleService.Update(this.Resource, article);
        }

        // PATCH: {resource}/Articles({id})
        [UserHasResourceAccess, UserHasPermission("Modify")]
        [EnableQuery, HttpPatch, ODataRoute("({id})")]
        public Article PartialUpdate(int id, [FromBody] Delta<Article> article)
        {
            this.ParseResource();
            return this._articleService.PartialUpdate(this.Resource, id, article);
        }

        // DELETE: {resource}/Articles({id})
        [UserHasResourceAccess, UserHasPermission("Remove")]
        [EnableQuery, HttpDelete, ODataRoute("({id})")]
        public void Delete(int id)
        {
            this.ParseResource();
            this._articleService.Delete(this.Resource, id);
            this.ODataDeleted(); // Set response headers
        }

    }


}

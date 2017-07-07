using System.Web.Http;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Infrastructure.Authorization.Attributes;
using TestRestfulAPI.Infrastructure.Controllers;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.RestApi.v1.Articles.Services;


namespace TestRestfulAPI.RestApi.v1.Articles.Controllers
{

    [RoutePrefix("api/v1")]
    public class ArticleController : ApiJsonController
    {
        private readonly ArticleService _articleService = GlobalServices.ArticleService;

        // GET: articles
        [UserHasRole("ProductOwner")]
        [HttpGet, Route("articles")]
        public IHttpActionResult Articles()
        {
           return Json(this._articleService.AllWithResourceContext(), "Collection");
        }

        // GET: articles/{INVALID}
        [HttpGet, Route("articles/{INVALID}")]
        public IHttpActionResult Articles(object invalid)
        {
            throw new InvalidEndpointException("This endpoint is not supported.");
        }

        // GET: {resource}/articles
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [HttpGet, Route("{resource}/articles")]
        public IHttpActionResult Articles(string resource)
        {
            return Json(this._articleService.All(resource));
        }

        // GET: {resource}/articles/{id}
        [UserHasResourceAccess]
        [UserHasPermission("Read")]
        [HttpGet, Route("{resource}/articles/{id}")]
        public IHttpActionResult Articles(string resource, int id)
        {
            return Json(this._articleService.Get(resource, id));
        }

        // POST: {resource}/articles
        [UserHasResourceAccess]
        [UserHasPermission("Create")]
        [HttpPost, Route("{resource}/articles")]
        public IHttpActionResult Create(string resource, Article article)
        {
            var newArticle = this._articleService.Create(resource, article);
            return JsonCreated(newArticle, newArticle.Id);
        }

        // PUT: {resource}/articles
        [UserHasResourceAccess]
        [UserHasPermission("Modify")]
        [HttpPut, Route("{resource}/articles/{id}")]
        public IHttpActionResult Update(string resource, int id, Article article)
        {
            var newArticle = this._articleService.Update(resource, article);
            return Json(newArticle);
        }


    }

    
}
  
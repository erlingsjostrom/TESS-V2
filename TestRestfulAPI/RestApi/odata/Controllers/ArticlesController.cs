using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using Microsoft.Web.Http;
using TestRestfulAPI.Entities.TESS;

namespace TestRestfulAPI.RestApi.odata.Controllers
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Articles")]
    public class ArticlesController : ResourceODataController
    {

        [EnableQuery, HttpGet, ODataRoute()]
        public IQueryable<Article> Get()
        {
            this.ParseResource();
            return GlobalServices.ArticleService.All(this.Resource);
        }

        [EnableQuery, HttpGet, ODataRoute("({id})")]
        public Article Get(int id)
        {
            this.ParseResource();
            return GlobalServices.ArticleService.Get(this.Resource, id);
        }
    }


}

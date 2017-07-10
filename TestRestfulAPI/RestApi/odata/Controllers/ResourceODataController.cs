using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData;

namespace TestRestfulAPI.RestApi.odata.Controllers
{
    public class ResourceODataController : ODataController
    {
        protected string Resource { get; set; }
        protected void ParseResource()
        {
            var res = HttpContext.Current.Request.RequestContext.RouteData.Values["resource"].ToString();
            if (!string.IsNullOrEmpty(res))
            {
                this.Resource = res;
            }
            else
            {
                throw new ResourceMissingException("Resource is missing");
            }

        }
        protected IHttpActionResult ODataCreated<T>(T result, int id) where T : class
        {
            var location = HttpContext.Current.Request.Url.AbsoluteUri + "(" + id + ")";
            return Created(location, result);
        }
        protected IEnumerable<string> ODataDeleted()
        {
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NoContent;
            return new List<string>();
        }
    }
}

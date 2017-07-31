using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using TestRestfulAPI.Infrastructure.Exceptions;

namespace TestRestfulAPI.Infrastructure.Controllers
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
        protected void ODataDeleted()
        {
            HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.NoContent;
        }
    }
}

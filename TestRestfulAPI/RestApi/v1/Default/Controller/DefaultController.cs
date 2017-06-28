using System.Collections.Generic;
using System.Web.Http;
using TestRestfulAPI.Infrastructure.Controllers;

namespace TestRestfulAPI.RestApi.v1.Default.Controller
{
    [RoutePrefix("api/v1")]
    public class DefaultController : ApiJsonController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Default()
        {
            var result = new Dictionary<string, string>()
            {
                {"Name", "TESS API - Tieto Essential Sale System API"},
                {"Version", "1.0"}
            };
            return Json(result, "info");
        }

        
    }
}

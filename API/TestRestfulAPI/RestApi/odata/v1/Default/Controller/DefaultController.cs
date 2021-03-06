﻿using System.Collections.Generic;
using System.Web.Http;
using TestRestfulAPI.Infrastructure.Controllers;

namespace TestRestfulAPI.RestApi.odata.v1.Default.Controller
{
    public class DefaultController : ApiJsonController
    {
        [HttpGet, Route("odata")]
        public IHttpActionResult Default()
        {
            var result = new Dictionary<string, string>()
            {
                {"Name", "TESS API - Tieto Essential Sale System API"},
                {"Version", "1.0.1"}
            };
            return Json(result, "info");
        }
       


    }
}

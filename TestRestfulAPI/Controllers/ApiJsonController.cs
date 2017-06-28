using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace TestRestfulAPI.Controllers
{
    public class ApiJsonController : ApiController
    {
        /// <summary>
        /// Gets a JSON object with the resource name of the content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">content to be serialized</param>
        /// <param name="resourceName">name of resource</param>
        /// <returns></returns>
        protected JsonResult<Dictionary<string, T>> Json<T>(T content, string resourceName)
        {
            return Json(new Dictionary<string, T>() { { resourceName, content } });
        }

        /// <summary>
        /// Get a JSON object with the resource name guessed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">content to be serialized</param>
        /// <returns></returns>
        protected JsonResult<Dictionary<string, T>> JsonAuto<T>(T content)
        {
            var resourceName = typeof(T).Name;
            return Json(new Dictionary<string, T>() { { resourceName, content } });
        }

        /// <summary>
        /// Get a JSON object with the resource name guessed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentList">contentList to be serialized</param>
        /// <returns></returns>
        protected JsonResult<Dictionary<string, ICollection<T>>> JsonAuto<T>(ICollection<T> contentList)
        {
            var resourceName = GetPluralizedName(contentList.First().GetType().Name);
            return Json(new Dictionary<string, ICollection<T>>() { { resourceName, contentList } });
        }

        /// <summary>
        /// Get a JSON object with the resource name guessed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentList">contentList to be serialized</param>
        /// <returns></returns>
        protected JsonResult<Dictionary<string, IEnumerable<T>>> JsonAuto<T>(IEnumerable<T> contentList)
        {
            var resourceName = GetPluralizedName(contentList.First().GetType().Name);
            return Json(new Dictionary<string, IEnumerable<T>>() { { resourceName, contentList } });
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        /// <summary>
        /// Help method to return a pluralized version of a name
        /// </summary>
        /// <param name="name">to pluralize</param>
        /// <returns></returns>
        private string GetPluralizedName(string name)
        {
            return PluralizationService.CreateService(CultureInfo.CreateSpecificCulture(ConfigurationManager.AppSettings.Get("ApiLocale")))
                .Pluralize(name);
        }
    }
}

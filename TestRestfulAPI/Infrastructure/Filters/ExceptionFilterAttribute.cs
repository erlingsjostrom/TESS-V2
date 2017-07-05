using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Helpers;

namespace TestRestfulAPI.Infrastructure.Filters
{
    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DoesNotExistException)
            {
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    HttpError errorMessage = new HttpError("The requested ID does not exist.")
                    {
                        { "HTTPStatus", HttpStatusCode.NotFound },
                        { "ErrorCode", 1 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, errorMessage);
                }
                else
                {
                    HttpError errorMessage = new HttpError("The requested ID does not exist.")
                    {
                        { "HTTPStatus", HttpStatusCode.NotFound },
                        { "ErrorCode", 1 }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, errorMessage);
                }   
            }
            if (context.Exception is InvalidDbContextTypeException)
            {
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    HttpError errorMessage = new HttpError("Invalid database.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 2 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                }
                else
                {
                    HttpError errorMessage = new HttpError("Invalid database.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 2 }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                }
            }
            if (context.Exception is InvalidDbConnectionFactoryInput)
            {
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    HttpError errorMessage = new HttpError("Invalid input to database connection factory.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 3 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                }
                else
                {
                    HttpError errorMessage = new HttpError("Invalid input to database connection factory.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 3 }
                    };
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                }
            }
        }
    }
}


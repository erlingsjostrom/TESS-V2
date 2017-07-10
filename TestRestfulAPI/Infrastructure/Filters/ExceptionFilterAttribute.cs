using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using TestRestfulAPI.Infrastructure.Authorization;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Helpers;
using TestRestfulAPI.RestApi.v1.Users.Exceptions;
using TestRestfulAPI.RestApi.v1.Articles.Controllers;

namespace TestRestfulAPI.Infrastructure.Filters
{
    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpError errorMessage;
            string msg;
            HttpStatusCode statusCode;
            int errorCode;

            if (context.Exception is DoesNotExistException)
            {
                msg = "The requested resource does not exist.";
                statusCode = HttpStatusCode.NotFound;
                errorCode = 1;
            }
            else if (context.Exception is InvalidDbContextTypeException)
            {
                msg = "Invalid database.";
                statusCode = HttpStatusCode.InternalServerError;
                errorCode = 2;
            }
            else if (context.Exception is InvalidDbConnectionFactoryInput)
            {
                msg = "Invalid input to database connection factory.";
                statusCode = HttpStatusCode.InternalServerError;
                errorCode = 3;
            }
            else if (context.Exception is UserDoesNotHaveRequiredRolesException)
            {
                msg = "This user does not have permission to view requested data with current role.";
                statusCode = HttpStatusCode.Forbidden;
                errorCode = 4;
            }
            else if (context.Exception is AlreadyExistException)
            {
                msg = "The resource does already exist.";
                statusCode = HttpStatusCode.BadRequest;
                errorCode = 5;
            }

            else if (context.Exception is InvalidEndpointException)
            {
                msg = "The requested endpoint is not valid.";
                statusCode = HttpStatusCode.BadRequest;
                errorCode = 6;
            }

            else if (context.Exception is MissingEndpointException)
            {
                msg = "The requested endpoint does not exist.";
                statusCode = HttpStatusCode.NotFound;
                errorCode = 7;
            }
            else
            {
                msg = context.Exception.Message;
                statusCode = HttpStatusCode.BadRequest;
                errorCode = -1;
            }

            if (GlobalVariables.IsDebuggingEnabled)
            {
                errorMessage = new HttpError(msg)
                {
                    { "HTTPStatus", statusCode },
                    { "ErrorCode", errorCode },
                    { "ExceptionType", context.Exception.GetType().Name },
                    { "ExceptionMessage", context.Exception.Message },
                    { "StackTrace", context.Exception.StackTrace }
                };
            }
            else
            {
                errorMessage = new HttpError(msg)
                {
                    { "HTTPStatus", statusCode },
                    { "ErrorCode", errorCode }
                };
            }
            context.Response = context.Request.CreateErrorResponse(statusCode, errorMessage);
        }
    }
}


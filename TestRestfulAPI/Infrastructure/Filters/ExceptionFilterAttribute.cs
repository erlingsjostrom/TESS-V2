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
using TestRestfulAPI.Infrastructure.Helpers.Authorization;

namespace TestRestfulAPI.Infrastructure.Filters
{
    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DoesNotExistException)
            {
                HttpError errorMessage;
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    errorMessage = new HttpError("The requested ID does not exist.")
                    {
                        { "HTTPStatus", HttpStatusCode.NotFound },
                        { "ErrorCode", 1 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                }
                else
                {
                    errorMessage = new HttpError("The requested ID does not exist.")
                    {
                        { "HTTPStatus", HttpStatusCode.NotFound },
                        { "ErrorCode", 1 }
                    };
                }
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, errorMessage);
                return;
            }
            if (context.Exception is InvalidDbContextTypeException)
            {
                HttpError errorMessage;
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    errorMessage = new HttpError("Invalid database.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 2 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                }
                else
                {
                    errorMessage = new HttpError("Invalid database.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 2 }
                    };
                }
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                return;
            }
            if (context.Exception is InvalidDbConnectionFactoryInput)
            {
                HttpError errorMessage;
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    errorMessage = new HttpError("Invalid input to database connection factory.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 3 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                }
                else
                {
                    errorMessage = new HttpError("Invalid input to database connection factory.")
                    {
                        { "HTTPStatus", HttpStatusCode.InternalServerError },
                        { "ErrorCode", 3 }
                    };
                }
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMessage);
                return;
            }
            if (context.Exception is UserDoesNotHaveRequiredRolesException)
            {
                HttpError errorMessage;
                if (GlobalVariables.IsDebuggingEnabled)
                {
                    errorMessage = new HttpError("This user does not have permission to view requested data with current role")
                    {
                        { "HTTPStatus", HttpStatusCode.Forbidden },
                        { "ErrorCode", 4 },
                        { "ExceptionType", context.Exception.GetType().Name },
                        { "ExceptionMessage", context.Exception.Message },
                        { "StackTrace", context.Exception.StackTrace }
                    };
                }
                else
                {
                    errorMessage = new HttpError("Invalid input to database connection factory.")
                    {
                        { "HTTPStatus", HttpStatusCode.Forbidden },
                        { "ErrorCode", 4 }
                    };
                }
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.Forbidden, errorMessage);
                return;
            }
        }
    }
}


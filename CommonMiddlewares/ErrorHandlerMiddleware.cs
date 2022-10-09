using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMiddlewares
{
    using EntityBase.Exceptions;
    using EntityBase.Poco.Responses;
    using Microsoft.AspNetCore.Http;
    using System.Net;
    using System.Text.Json;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var res = new MessageResponse();
                switch (error)
                {
                    case CustomErrorException e:
                        res.Message = (error as CustomErrorException).Message;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        res.Message = "Error Occurred During Operation";
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(Response<MessageResponse>.Fail(res,response.StatusCode));
                await response.WriteAsync(result);
            }
        }
    }
}

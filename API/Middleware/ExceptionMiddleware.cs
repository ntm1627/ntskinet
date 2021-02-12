using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
         IHostEnvironment env) //RequestDelegate process http request
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);// if there is no exception request go move the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);  //this will log the error to the console
                context.Response.ContentType="application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;  //500 server err

                var response=_env.IsDevelopment()  //ternary operator
                    ? new ApiException((int) HttpStatusCode.InternalServerError,  //for developer 
                    ex.Message, ex.StackTrace.ToString())               //for production
                    : new ApiException ((int)HttpStatusCode.InternalServerError);

                var options=new JsonSerializerOptions
                {
                    PropertyNamingPolicy=JsonNamingPolicy.CamelCase   //By default json return Pascal and that should be changed to Camelcase 
                };
                var json=JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);

            }
        }

    }
}
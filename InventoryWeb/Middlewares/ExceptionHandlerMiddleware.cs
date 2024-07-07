using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventoryWeb.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)  //Because this is middleware, we have to provide a method (InvokeAsync).
        {                                                   //This method name must be 'InvokeAsync' because our middleware is expecting this method.
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var requestPath = context.Request.Path;

                if (requestPath.StartsWithSegments("/api")) //context.Request.Path.Value.ToLower().StartsWith("/api")
                {
                    // It's an API request

                    var response = new ProblemDetails
                    {
                        Status = 500,
                        Detail = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                        Title = ex.Message
                    };

                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
                }
                else
                {
                    // It's an MVC request
                    context.Response.Redirect("/Home/Error");
                }
                
            }
        }
    }
}

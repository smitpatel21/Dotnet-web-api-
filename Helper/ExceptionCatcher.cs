using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace WebAPI.Helper
{
    public class ExceptionCatcher : IMiddleware
    {
        /*public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(
                  async context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      var ex = context.Features.Get<IExceptionHandlerFeature>();
                      if (ex != null)
                      {
                          await context.Response.WriteAsync(ex.Error.Message);
                      }
                  });
            });
        }*/

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.Message);
            }

        }

    }
}

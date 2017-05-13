using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

public class MyMiddleware
{
  private readonly RequestDelegate _next;
  private readonly IHostingEnvironment _env;

  public MyMiddleware(RequestDelegate next, IHostingEnvironment env)
  {
    _next = next;
    _env = env;
  }

  public Task Invoke(HttpContext httpContext)
  {
    httpContext.Response.Headers.Add("Environment", _env.EnvironmentName);
    
    return _next(httpContext);
  }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MyMiddlewareExtensions
{
  public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<MyMiddleware>();
  }
}
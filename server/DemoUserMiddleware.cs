using System.Security.Claims;

namespace server;

// spauldo_techture was designed with Authentication and Authorization in mind. 
// Audit operations require a user for logging.
// Manually setting user here to avoid spauldo_techture throwing exceptions.
public class DemoUserMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var identity = new ClaimsIdentity(new Claim[] { new(ClaimTypes.Name, "Demo User") });
        context.User = new ClaimsPrincipal(identity);

        await _next(context);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class DemoUserMiddlewareExtensions
{
    public static IApplicationBuilder UseDemoUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DemoUserMiddleware>();
    }
}
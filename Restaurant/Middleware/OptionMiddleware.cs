namespace Restaurant.Middleware;

public class OptionsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public OptionsMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public Task Invoke(HttpContext context)
    {
        _logger.LogDebug("Start another");
        return BeginInvoke(context);
    }

    private Task BeginInvoke(HttpContext context)
    {
        _logger.LogDebug("Start option middleware");
        if (context.Request.Method != "OPTIONS") return _next.Invoke(context);

        _logger.LogDebug("Inside option middleware");
        context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"]! });
        context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
        context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
        context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
        context.Response.StatusCode = 200;

        _logger.LogDebug("Response option middleware");
        return context.Response.WriteAsync("OK");
    }
}

public static class OptionsMiddlewareExtensions
{
    public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OptionsMiddleware>();
    }
}
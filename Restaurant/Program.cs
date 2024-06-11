using Restaurant;
using Restaurant.Application;
using Restaurant.Infrastructure;
using Restaurant.Infrastructure.Migrations;
using Restaurant.Infrastructure.WebSocketHubs;
using Restaurant.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog();

    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddSignalR().AddJsonProtocol();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.ApplyMigrations();
    }

    app.UseOptions();
    app.UseCors(corsBuilder => corsBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders()
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .DisallowCredentials());
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseExceptionHandler("/error");
    app.MapHub<OrderHub>("notification");
    app.MapControllers();
}

try
{
    Console.WriteLine("NEW VERSION");
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
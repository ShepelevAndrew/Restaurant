using Restaurant;
using Restaurant.Application;
using Restaurant.Infrastructure;
using Restaurant.Infrastructure.Migrations;
using Restaurant.Infrastructure.WebSocketHubs;
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
    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
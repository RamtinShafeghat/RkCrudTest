using Mc2.CrudTest.Api;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("RayanKar API starting");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration
       .WriteTo.Console()
       .ReadFrom.Configuration(context.Configuration));

var app = builder.ConfigureServices()
                 .ConfigurePipeline();

await app.ResetDatabaseAsync();
await app.ResetDatabaseIdentityAsync();

app.Run();

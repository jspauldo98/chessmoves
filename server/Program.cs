using Microsoft.EntityFrameworkCore;
using HashidsNet;
using Hangfire;
using Hangfire.MySql;

using server.Services;
using server.Context;
using server;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("connectionStrings.json");

string connectionString = builder.Configuration.GetConnectionString("Connection");

// Hangfire
var hangfireConnection = builder.Configuration.GetConnectionString("HangfireConnection");
builder.Services.AddHangfire(conf => conf
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseStorage(new MySqlStorage(hangfireConnection, new MySqlStorageOptions {
        TablesPrefix = "hangfire",
        QueuePollInterval = TimeSpan.FromSeconds(1),
        JobExpirationCheckInterval = TimeSpan.FromHours(1),
        CountersAggregateInterval = TimeSpan.FromMinutes(5),
        DashboardJobListLimit = 50000,
        TransactionTimeout = TimeSpan.FromMinutes(1),
    })));
builder.Services.AddHangfireServer();

builder.Services.AddDbContext<PuzzlesDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

string[] origins = builder.Configuration.GetSection("Origins").Get<string[]>() 
    ?? throw new Exception($"Error retrieving Origins array.");

builder.Services.AddCors(options =>
{
    // For demo no big deal, but would want to restrict this further
    options.AddPolicy("Cors-Policy", p =>
    {
            p
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(origins)
        ;
    });
});

var hashids = new Hashids(builder.Configuration.GetConnectionString("SaltKey"), 11);

builder.Services.AddTransient<IHashids>(x => hashids);
builder.Services.RegisterServices();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseRouting();
app.UseCors("Cors-Policy");

app.UseDemoUser();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

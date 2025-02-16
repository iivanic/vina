using DBcs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Scalar.AspNetCore;
using vina.Server;
using vina.Server.Config;
using vina.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AuthService>(); // Register AuthService for dependency injection
builder.Services.AddScoped<EmailService>(); // 

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

string connectionString = (builder.Configuration.GetConnectionString("DefaultConnection") ?? "").Replace("{DATABASE}", builder.Configuration.GetSection("AppSettings").Get<AppSettings>()?.DatabaseName);
builder.Services.AddSingleton(connectionString);

builder.Services.AddScoped<IDBcs>(provider => 
{
    var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
    var connectionString = (builder.Configuration.GetConnectionString("DefaultConnection") ?? "").Replace("{DATABASE}", appSettings?.DatabaseName);
    return new DBcs.DBcs(connectionString);
});
var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Scalar UI at http://localhost:5186/scalar/v1 when running your application
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//Seeder.Instance.DbReCreate().GetAwaiter().GetResult();
//Seeder.Instance.DbSeed().GetAwaiter().GetResult();
//var c = Seeder.Instance.GetClasses().GetAwaiter().GetResult();

app.Run();

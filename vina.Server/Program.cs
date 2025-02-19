using DBcs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Scalar.AspNetCore;
using vina.Server;
using vina.Server.Config;
using vina.Server.Controllers;

var builder = WebApplication.CreateBuilder(args);

string connectionString = (
    builder.Configuration.GetConnectionString("DefaultConnection") ?? "")
    .Replace(
        "{DATABASE}",
        builder.Configuration.GetSection("AppSettings").Get<AppSettings>()?.DatabaseName
    );

// Add services to the container.

// ---------------identity ----------------
builder.Services.AddIdentityCore<IdentityUser>();
builder.Services.AddEntityFrameworkStores<NPDataContext>();
var UserType = builder.UserType;
var provider = typeof(NPTokenProvider<>).MakeGenericType(UserType);
builder.AddTokenProvider("NPTokenProvider", provider);
services.AddDbContext<NPDataContext>(options => options.UseNpgsql(connectionString));

services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ExternalScheme;
});
builder.Services.AddTransient<IdentityDbContext, NPDataContext>(); // Register IdentityDbContext for dependency injection
// -----------------------------------------

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AuthService>(); // Register AuthService for dependency injection
builder.Services.AddScoped<EmailService>(); // 

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton(connectionString);

builder.Services.AddScoped<IDBcs>(provider => {return new DBcs.DBcs(connectionString);});
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

app.Routing.Register<NoPasswordController>(app);

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

//Seeder.Instance.DbReCreateEmpty().GetAwaiter().GetResult();
Seeder.Instance.DbEnsureCratedAndSeed(app).GetAwaiter().GetResult();
//var c = Seeder.Instance.GetClasses().GetAwaiter().GetResult();

app.Run();

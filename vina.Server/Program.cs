using DBcs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Scalar.AspNetCore;
using vina.Server;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using vina.Server.Config;
using vina.Server.Controllers;
using vina.Server.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connectionString = (
    builder.Configuration.GetConnectionString("DefaultConnection") ?? "")
    .Replace(
        "{DATABASE}",
        builder.Configuration.GetSection("AppSettings").Get<AppSettingsOptions>()?.DatabaseName
    );

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddProvider(new LoggerDatabaseProvider(connectionString));

// Add services to the container.

// ---------------identity ----------------
var ic = builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NPDataContext>()
    .AddDefaultTokenProviders();
    
var UserType = ic.UserType;
var provider = typeof(NPTokenProvider<>).MakeGenericType(UserType);
ic.AddTokenProvider("NPTokenProvider", provider);
builder.Services.AddDbContext<NPDataContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    var jwtKey = builder.Configuration["JWT:Key"];
    if (string.IsNullOrEmpty(jwtKey))
        throw new InvalidOperationException("JWT:Key configuration is missing or empty.");
    var Key = Encoding.UTF8.GetBytes(jwtKey);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});
//for captcha
builder.Services.AddMemoryCache();
builder.Services.AddTransient<NPDataContext>(); // Register IdentityDbContext for dependency injection
// -----------------------------------------

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // This stops System.Text.Json from converting property names to camelCase
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}); ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AuthService>(); // Register AuthService for dependency injection
builder.Services.AddScoped<EmailService>(); // 

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
    options.SlidingExpiration = true;
    options.LoginPath = new PathString("/Account");
    options.ReturnUrlParameter = "returnURL";
    //other properties
});



builder.Services.Configure<AppSettingsOptions>(builder.Configuration.GetSection(AppSettingsOptions.AppSettings));
//builder.Services.AddSingleton<AppSettingsOptions>();
builder.Services.AddSingleton(connectionString);

builder.Services.AddSingleton<IDBcs>(provider => {return new DBcs.DBcs(connectionString);});

//enable mail sending
builder.Services.AddTransient<IEmailSender, MailJetEmailService>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);


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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapFallbackToFile("/index.html");

//Seeder.Instance.DbReCreateEmpty().GetAwaiter().GetResult();
Seeder.Instance.DbEnsureCratedAndSeed(app).GetAwaiter().GetResult();
//var c = Seeder.Instance.GetClasses(["DBLog"],"select * from public.logs").GetAwaiter().GetResult();

app.Run();

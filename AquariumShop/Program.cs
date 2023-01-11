using AquariumShop.Config;
using AquariumShop.Seed;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Services.User;
using System.Reflection;
using System.Text;

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger = log;
log.Information("Hello, Serilog!");

try
{

    var builder = WebApplication.CreateBuilder(args);


    // Add services to the container.
    builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddLogging(x =>
    {
        x.ClearProviders();
        x.AddSerilog(dispose: true);
    });
    
    builder.Services.AddEndpointsApiExplorer();
    
    SwaggerConfig.Handle(builder);
    builder.Services.AddDbContext<AquariumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

    builder.Services.AddAuthentication()
        .AddJwtBearer(config =>
        {
            config.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true
            };
        });

    builder.Services.AddIdentity<ApiUser, IdentityRole>(x =>
    {
        x.Password.RequireDigit = false;
        x.Password.RequiredLength = 2;
        x.Password.RequireUppercase = false;
        x.Password.RequireLowercase = false;
        x.Password.RequireNonAlphanumeric = false;
        x.Password.RequiredUniqueChars = 0;
        x.Lockout.AllowedForNewUsers = true;
        x.Lockout.MaxFailedAccessAttempts = 5;
        x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
        x.SignIn.RequireConfirmedAccount = false;
    })
        .AddEntityFrameworkStores<AquariumDbContext>()
        .AddSignInManager<SignInManager<ApiUser>>()
        .AddDefaultTokenProviders();


    builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
    builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
    builder.Services.AddScoped<IRepository<Cart>, Repository<Cart>>();
    builder.Services.AddScoped<IProductRepository<Product>, ProductRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();

    builder.Services.AddScoped<ISeeder<Category>, CategorySeeder>();
    builder.Services.AddScoped<ISeeder<Product>, ProductSeeder>();

    builder.Services.AddCors(c =>
    {
        c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod());
    });

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        using var scope = app.Services.CreateScope();
        var categorySeeder = scope.ServiceProvider.GetRequiredService<ISeeder<Category>>();
        await categorySeeder.SeedAsync();

        var productSeeder = scope.ServiceProvider.GetRequiredService<ISeeder<Product>>();
        await productSeeder.SeedAsync();

        await new RoleSeeder().Seed(app.Services.CreateScope().ServiceProvider);
    }
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    app.UseHttpsRedirection();


    app.UseAuthentication();
    app.UseAuthorization();


    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }

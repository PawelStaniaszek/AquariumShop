using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using AquariumShop.Seed;
using Microsoft.AspNetCore.Identity;
using Services.User;
using AspNet.Security.OAuth.Validation;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AquariumShop.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
SwaggerConfig.Handle(builder);

builder.Services.AddDbContext<AquariumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddAuthentication()
    //.AddCookie("Identity.Application")
    .AddJwtBearer(config =>
    {
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddIdentity<ApiUser, IdentityRole>(x => {
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

//builder.Services.AddScoped<SignInManager<ApiUser>>();


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
    using (var scope = app.Services.CreateScope())
    {
        var categorySeeder = scope.ServiceProvider.GetRequiredService<ISeeder<Category>>();
        await categorySeeder.SeedAsync();

        var productSeeder = scope.ServiceProvider.GetRequiredService<ISeeder<Product>>();
        await productSeeder.SeedAsync();

        await new RoleSeeder().Seed(app.Services.CreateScope().ServiceProvider);
    }
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

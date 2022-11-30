using Domain.Models;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using AquariumShop.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AquariumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IProductRepository<Product>, ProductRepository>();

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
    }
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();

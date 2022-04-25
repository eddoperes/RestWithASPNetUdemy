using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Services;
using RestWithASPNetUdemy.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("SQLServerConnectionString");
builder.Services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

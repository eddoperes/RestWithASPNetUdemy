using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RestWithASPNetUdemy.Repository.Generic;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("SQLServerConnectionString");
builder.Services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(connection));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
//builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
//builder.Services.AddScoped<IBookRepository, BookRepositoryImplementation>();

builder.Services.AddScoped( typeof(IRepository<>),typeof( GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connection)
{
    try 
    {
        var evolveConnection = new Microsoft.Data.SqlClient.SqlConnection(connection);
        var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string>{"db/migrations", "db/dataset"},
            IsEraseDisabled = true
        };
        evolve.Migrate();  
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex); 
        throw;
    }
}

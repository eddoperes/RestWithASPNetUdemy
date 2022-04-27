using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RestWithASPNetUdemy.Repository.Generic;
using System.Net.Http.Headers;
using RestWithASPNetUdemy.Hypermedia.Filters;
using RestWithASPNetUdemy.Hypermedia.Enricher;

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

builder.Services.AddMvc(options =>
{ 
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

var filterOptions = new HypermediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

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

app.MapControllerRoute("DefaultApi","{controller=value}/{id?}");

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

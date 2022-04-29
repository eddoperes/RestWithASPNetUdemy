using RestWithASPNetUdemy.Model.Context;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RestWithASPNetUdemy.Repository.Generic;
using System.Net.Http.Headers;
using RestWithASPNetUdemy.Hypermedia.Filters;
using RestWithASPNetUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using RestWithASPNetUdemy.Service;
using RestWithASPNetUdemy.Service.Implementations;
using RestWithASPNetUdemy.Repository;
using RestWithASPNetUdemy.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var tokenConfiguration = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
        builder.Configuration.GetSection("TokenConfiguration")
    ).Configure(tokenConfiguration);
builder.Services.AddSingleton(tokenConfiguration);
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
    };
});
builder.Services.AddAuthorization(auth => {
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().Build()); 

});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

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

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1",
    new OpenApiInfo
    {
        Title = "Rest With ASP Net Udemy",
        Description = "API Restfull",       
        Contact = new OpenApiContact
        {
            Name = "Eduardo Peres",
            Url = new Uri("https://github.com/eddoperes/RestWithASPNetUdemy")
        }
    });
   
});

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
//builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
//builder.Services.AddScoped<IBookRepository, BookRepositoryImplementation>();

builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddTransient<ITokenService, TokenServiceImplementation>();

builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

builder.Services.AddScoped( typeof(IRepository<>),typeof( GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(); 

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest With ASP Net Udemy - v1");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option); 

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


/*
 * Add authentication to swagger
 * 
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                  Enter 'Bearer' [space] and then your token in the text input below.
                  \r\n\r\nExample: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
});

c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          },
          Scheme = "oauth2",
          Name = "Bearer",
          In = ParameterLocation.Header,

        },
        new List<string>()
      }
    });
*/
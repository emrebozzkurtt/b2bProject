using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using NLog;
using NLog.Web;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encryption;
using Core.Utilities.IoC;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//services
IServiceCollection service = builder.Services;
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.

//use service for extending. this addDependecyResolver is extension.


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

service.AddDependencyResolvers( new ICoreModule[]
{
    new CoreModule(),
});

//Autofac integretion for resolve of dependency
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>
        (builder => builder.RegisterModule(new AutofacBusinessModule()));

//NLog integretion for resolve of dependency
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
builder.Logging.ClearProviders();
builder.Host.UseNLog();


//JWT configuration
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

//app
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.ConfigureCustomExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();



app.MapControllers();

app.Run();

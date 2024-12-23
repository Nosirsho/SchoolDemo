using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using School.API;
using School.API.Endpoints;
using School.API.Extensions;
using School.Infrastructure;
using School.Persistence;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;
    var configuration = builder.Configuration;
    services.AddApiAuthentication(configuration);

    //NLog: Установка логирования в DI
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddControllers();
    services.AddApplication();
    
    builder.WebHost.UseUrls("http://localhost:5296");

    var app = builder.Build();
    
    app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    
    using var scope = app.Services.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
    await dbContext.Database.EnsureCreatedAsync();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();
    app.AddMappedExtensions();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, e.Message);
}
finally
{
    LogManager.Shutdown();
}


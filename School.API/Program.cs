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

    //NLog: Установка логирования в DI
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection(nameof(SecurityKey)).Value))
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["test_token"];
                    return Task.CompletedTask;
                }
            };
        });
   
    
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


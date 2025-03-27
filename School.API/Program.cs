using NLog;
using NLog.Web;
using School.API;
using School.API.Extensions;
using School.Core.Enums;
using School.Core.Model.SmsSender;
using School.Infrastructure;
using School.Persistence;
using School.Persistence.Mappings;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    //NLog: Установка логирования в DI
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    builder.WebHost.UseUrls("http://localhost:5296");
    
    var services = builder.Services;
    var configuration = builder.Configuration;
    
    services.AddApiAuthentication(configuration);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.Configure<NotificationSettings>(configuration.GetSection("NotificationSettings"));
    services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
    services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));
    services.AddAutoMapper(typeof(DataBaseMappings));
    services.AddHttpClient("SMS", o =>
    {
        o.BaseAddress = new Uri("https://api.osonsms.com/");
    });
    
    //services.AddPersistence(configuration);
    services.AddApplication();

    services.AddControllers();
    
    // services.AddDbContext<SchoolDbContext>(options =>
    // {
    //     options.UseNpgsql(configuration.GetConnectionString(nameof(SchoolDbContext)));
    //     options.LogTo(System.Console.WriteLine);
    // });
    
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
    app.MapGet("get", () => Results.Ok("Hello World!")).RequirePermissions(Permission.Read);
    app.MapPost("post", () => Results.Ok("Hello World!")).RequirePermissions(Permission.Create);
    app.MapPut("put", () => Results.Ok("Hello World!")).RequirePermissions(Permission.Update);
    app.MapDelete("delete", () => Results.Ok("Hello World!")).RequirePermissions(Permission.Delete);
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


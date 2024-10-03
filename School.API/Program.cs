using NLog;
using NLog.Web;
using School.API;
using School.Persistence;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    //NLog: Установка логирования в DI
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddApplication();

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

    app.MapControllers();

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


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace School.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SchoolDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(SchoolDbContext)));
        });

        // services.AddScoped<ICourseRepository, CourseRepository>();
        // services.AddScoped<ILessonsRepository, LessonsRepository>();
        // services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}
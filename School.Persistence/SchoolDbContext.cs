using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using School.Core.Model;
using School.Persistence.Configurations;
using School.Persistence.Entities;

namespace School.Persistence;

public class SchoolDbContext: DbContext
{
    private readonly IConfiguration _configuration;
    private readonly AuthorizationOptions _authOptions;

    public SchoolDbContext()
    {
        
    }
    public SchoolDbContext(IConfiguration configuration,
        AuthorizationOptions authOptions)
    {
        _configuration = configuration;
        _authOptions = authOptions;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=school;Username=postgres;Password=postgres;");
        optionsBuilder.LogTo(System.Console.WriteLine);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new ParentConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new GradeLevelConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
}
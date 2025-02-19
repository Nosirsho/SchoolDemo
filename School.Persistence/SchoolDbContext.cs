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
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=school3;Username=postgres;Password=postgres;");
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
        modelBuilder.ApplyConfiguration(new GradeBookConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<ParentEntity> Parents => Set<ParentEntity>();
    public DbSet<TeacherEntity> Teachers => Set<TeacherEntity>();
    public DbSet<GradeLevelEntity> GradeLevels => Set<GradeLevelEntity>();
    public DbSet<LessonEntity> Lessons => Set<LessonEntity>();
    public DbSet<ScheduleEntity> Schedules => Set<ScheduleEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<GradeBookEntity> GradeBooks => Set<GradeBookEntity>();
}
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Persistence.Configurations;

namespace School.Persistence;

public class SchoolDbContext(DbContextOptions<SchoolDbContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new ParentConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new GradeLevelConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<GradeLevel> GradeLevels => Set<GradeLevel>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<User> Users => Set<User>();
}
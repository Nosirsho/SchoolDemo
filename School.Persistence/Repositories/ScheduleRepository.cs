using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class ScheduleRepository : IScheduleStore
{
    private readonly SchoolDbContext _schoolDbContext;

    public ScheduleRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public async Task<Schedule?> GetById(Guid id)
    {
        return await _schoolDbContext.Schedules.FindAsync(id);
    }

    public async Task<Schedule?> GetByGradeLevelAndDay(Guid gradeLevelId, DayOfWeek day, int number)
    {
        return await _schoolDbContext.Schedules
            .Where(s => s.GradeLevelId == gradeLevelId && s.DayOfWeek == day && s.Number == number && s.IsActive)
            .FirstOrDefaultAsync();
    }
    public async Task<IReadOnlyList<ScheduleDto>> GetGradeDayCollection(Guid gradeLevelId, DayOfWeek day)
    {
        var result = await _schoolDbContext.Schedules
                .Where(s => s.GradeLevelId == gradeLevelId && s.DayOfWeek == day && s.IsActive)
                .Select(s => new ScheduleDto
                {
                    DayOfWeek = s.DayOfWeek,
                    GradeLevel = s.GradeLevel.Name,
                    GradeLevelId = s.GradeLevel.Id,
                    LessonId = s.Lesson.Id,
                    Number = s.Number,
                })
                .ToListAsync();
        return result;
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetAll()
    {
        var result = await _schoolDbContext.Schedules
            .Where(s=>s.IsActive)
            .Select(s => new ScheduleDto
            {
                DayOfWeek = s.DayOfWeek,
                GradeLevel = s.GradeLevel.Name,
                GradeLevelId = s.GradeLevel.Id,
                LessonId = s.Lesson.Id,
                Number = s.Number,
            })
            .ToListAsync();
        return result;
    }

    public async Task<Schedule> Update(Guid id, Schedule schedule)
    {
        var curSchedule = await _schoolDbContext.Schedules.FindAsync(id);
        if (curSchedule==null) throw new NullReferenceException("Schedule not found");
        
        curSchedule.Id = schedule.Id;
        curSchedule.LessonId = schedule.LessonId;
        curSchedule.GradeLevel = schedule.GradeLevel;
        curSchedule.Teacher = schedule.Teacher;
        curSchedule.DayOfWeek = schedule.DayOfWeek;
        await _schoolDbContext.SaveChangesAsync();
        return curSchedule;
    }
    
    public async Task DeActivateAllSchedule()
    {
        using (var transaction = await _schoolDbContext.Database.BeginTransactionAsync())
        {
            var schedules = await _schoolDbContext.Schedules.Where(s=>s.IsActive).ToListAsync();
            foreach (var schedule in schedules)
            {
                schedule.IsActive = false;
                schedule.ModifiedOn = DateTime.Now;
            }
            await _schoolDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
    }

    public async Task Add(Schedule schedule)
    {
        schedule.CreatedOn = DateTime.Now;
        await _schoolDbContext.Schedules.AddAsync(schedule);
        await _schoolDbContext.SaveChangesAsync();
    }
}
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

    public async Task<IReadOnlyList<ScheduleDto>> GetAll()
    {
        var result = await _schoolDbContext.Schedules
            .Select(s => new ScheduleDto
            {
                DayOfWeek = s.DayOfWeek,
                GradeLevel = s.GradeLevel.Name,
                Lesson = s.Lesson.Name,
                Number = s.Number,
            }).ToListAsync();
            
        
        
        
        return result;
    }

    public async Task<Schedule> Update(Schedule schedule)
    {
        var curSchedule = await _schoolDbContext.Schedules.FindAsync(schedule.Id);
        if (curSchedule==null) throw new NullReferenceException("Schedule not found");
        
        curSchedule.Id = schedule.Id;
        curSchedule.Lesson = schedule.Lesson;
        curSchedule.GradeLevel = schedule.GradeLevel;
        curSchedule.Teacher = schedule.Teacher;
        curSchedule.DayOfWeek = schedule.DayOfWeek;
        await _schoolDbContext.SaveChangesAsync();
        return curSchedule;
    }

    public async Task Add(Schedule schedule)
    {
        await _schoolDbContext.Schedules.AddAsync(schedule);
        await _schoolDbContext.SaveChangesAsync();
    }
}
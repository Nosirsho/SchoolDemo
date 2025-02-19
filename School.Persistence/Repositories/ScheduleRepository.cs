using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class ScheduleRepository : IScheduleStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public ScheduleRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }

    public async Task<Schedule?> GetById(Guid id)
    {
        return _mapper.Map<Schedule>(await _schoolDbContext.Schedules.FindAsync(id));
    }

    public async Task<IReadOnlyList<ScheduleDto>> GetAll()
    {
        var result = await _schoolDbContext.Schedules
            .Select(s => new ScheduleDto
            {
                DayOfWeek = s.DayOfWeek,
                GradeLevel = s.GradeLevel.Name,
                GradeLevelId = s.GradeLevel.Id,
                LessonId = s.Lesson.Id,
                Number = s.Number,
            }).ToListAsync();
        return result;
    }

    public async Task<Schedule> Update(Schedule schedule)
    {
        var curSchedule = await _schoolDbContext.Schedules.FindAsync(schedule.Id);
        if (curSchedule==null) throw new NullReferenceException("Schedule not found");
        var scheduleEntity = _mapper.Map<ScheduleEntity>(schedule); 
        
        curSchedule.Id = scheduleEntity.Id;
        curSchedule.Lesson = scheduleEntity.Lesson;
        curSchedule.GradeLevel = scheduleEntity.GradeLevel;
        curSchedule.Teacher = scheduleEntity.Teacher;
        curSchedule.DayOfWeek = schedule.DayOfWeek;
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Schedule>(curSchedule);
    }

    public async Task Add(Schedule schedule)
    {
        var scheduleEntity = _mapper.Map<ScheduleEntity>(schedule);
        await _schoolDbContext.Schedules.AddAsync(scheduleEntity);
        await _schoolDbContext.SaveChangesAsync();
    }
}
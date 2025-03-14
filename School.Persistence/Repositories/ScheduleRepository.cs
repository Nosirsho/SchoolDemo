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
    
    public async Task<Schedule?> GetByGradeLevelAndDay(Guid gradeLevelId, DayOfWeek day, int number)
    {
        var result = await _schoolDbContext.Schedules
            .Where(s => s.GradeLevelId == gradeLevelId && s.DayOfWeek == day && s.Number == number && s.IsActive)
            .FirstOrDefaultAsync(); 
        return _mapper.Map<Schedule>(result);
    }
    
    public async Task<IReadOnlyList<ScheduleDto>> GetGradeDayCollection(Guid gradeLevelId, DayOfWeek day)
    {
        var result = await _schoolDbContext.Schedules
            .Where(s => s.GradeLevelId == gradeLevelId && s.DayOfWeek == day && s.IsActive)
            .OrderBy(s=>s.Number)
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

    public async Task<Schedule> Update(Guid id, Schedule schedule)
    {
        var curSchedule = await _schoolDbContext.Schedules.FindAsync(id);
        if (curSchedule==null) throw new NullReferenceException("Schedule not found");
        var scheduleEntity = _mapper.Map<ScheduleEntity>(schedule); 
        
        scheduleEntity.Id = schedule.Id;
        scheduleEntity.LessonId = schedule.LessonId;
        scheduleEntity.GradeLevelId = schedule.GradeLevelId;
        scheduleEntity.TeacherId = schedule.TeacherId;
        scheduleEntity.DayOfWeek = schedule.DayOfWeek;
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Schedule>(scheduleEntity);
    }

    public async Task Add(Schedule schedule)
    {
        schedule.CreatedOn = DateTime.Now.ToUniversalTime();
        schedule.IsActive = true;
        var scheduleEntity = _mapper.Map<ScheduleEntity>(schedule);
        await _schoolDbContext.Schedules.AddAsync(scheduleEntity);
        await _schoolDbContext.SaveChangesAsync();
    }
    
    public async Task DeActivateAllSchedule()
    {
        try
        {
            var schedules = await _schoolDbContext.Schedules.Where(s => s.IsActive).ToListAsync();
            foreach (var schedule in schedules)
            {
                schedule.IsActive = false;
                schedule.ModifiedOn = DateTime.Now.ToUniversalTime();
            }

            await _schoolDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAllSchedules(List<Schedule> schedules)
    {
        using (var transaction = await _schoolDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await DeActivateAllSchedule();
                var scheduleEntities = schedules.Select(schedule => _mapper.Map<ScheduleEntity>(schedule));
                foreach (var item in scheduleEntities)
                {
                    item.IsActive = true;
                    item.CreatedOn = DateTime.Now.ToUniversalTime();
                    item.TeacherId = new Guid("1575786b-c1a9-4435-93d2-6de9c02622ad");
                    await _schoolDbContext.Schedules.AddAsync(item);
                }
                await _schoolDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await transaction.RollbackAsync();
                throw new Exception($"Failed to save schedules: {e.Message}");
            }
        }
    }
}
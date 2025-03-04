using School.Core.Model;

namespace School.Core.Stores;

public interface IScheduleStore
{
    public Task<Schedule?> GetById(Guid id);
    public Task<IReadOnlyList<ScheduleDto>> GetAll();
    public Task<Schedule> Update(Guid id,Schedule schedule);
    public Task Add(Schedule schedule);
    public Task<Schedule?> GetByGradeLevelAndDay(Guid gradeLevelId, DayOfWeek day, int number);
    public Task<IReadOnlyList<ScheduleDto>> GetGradeDayCollection(Guid gradeLevelId, DayOfWeek day);
    public Task DeActivateAllSchedule();
    public Task AddAllSchedules(List<Schedule> schedules);
}
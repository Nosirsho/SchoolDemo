using School.Core.Model;

namespace School.Core.Stores;

public interface IScheduleStore
{
    public Task<Schedule?> GetById(Guid id);
    public Task<IReadOnlyList<ScheduleDto>> GetAll();
    public Task<Schedule> Update(Schedule schedule);
    Task Add(Schedule schedule);
}
using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class ScheduleService
{
    private readonly IScheduleStore _scheduleStore;


    public ScheduleService(IScheduleStore scheduleStore)
    {
        _scheduleStore = scheduleStore;
    }
    
    public async Task<Schedule?> GetById(Guid scheduleId)
    {
        return await _scheduleStore.GetById(scheduleId);
    }

    public async Task<IReadOnlyList<Schedule>> GetAll()
    {
        return await _scheduleStore.GetAll();
    }

    public async Task<Schedule> Update(Schedule schedule)
    {
        return await _scheduleStore.Update(schedule);
    }

    public async Task Create(Schedule schedule)
    {
        await _scheduleStore.Add(schedule);
    }
}
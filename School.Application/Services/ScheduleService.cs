using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class ScheduleService
{
    private readonly IScheduleStore _scheduleStore;
    private readonly IGradeLevelStore _gradeLevelStore;


    public ScheduleService(IScheduleStore scheduleStore, IGradeLevelStore gradeLevelStore)
    {
        _scheduleStore = scheduleStore;
        _gradeLevelStore = gradeLevelStore;
    }
    
    public async Task<Schedule?> GetById(Guid scheduleId)
    {
        return await _scheduleStore.GetById(scheduleId);
    }

    async Task<IReadOnlyList<ScheduleDto>> GetAll2()
    {
        var gradeLevelCollection = await _gradeLevelStore.GetAll();
        var gradeScheduleCollection = new List<ScheduleDto>();
        foreach (var gradeLevel in gradeLevelCollection)
        {
            for (int i = 1; i < 7; i++)
            {
                var schedules = await _scheduleStore.GetGradeDayCollection(gradeLevel.Id, (DayOfWeek)i);
                if (schedules.Any())
                {
                    gradeScheduleCollection.AddRange(schedules);
                }
                else
                {
                    gradeScheduleCollection.Add(new ScheduleDto()
                    {
                        DayOfWeek = (DayOfWeek)i,
                        GradeLevel = gradeLevel.Name,
                        GradeLevelId = gradeLevel.Id,
                        Number = 1
                    });
                }
                
            }
        }
        return gradeScheduleCollection;
    }

    public async Task<IReadOnlyList<GradeSchedule>> GetAll()
    {
        var schedules = await GetAll2();
        var schedulesCollection = schedules
            .GroupBy(s => new { s.GradeLevel, s.GradeLevelId})
            .Select(g => new
            {
                Grade = g.Key.GradeLevel,
                GradeLevelId = g.Key.GradeLevelId,
                Schedule = g.GroupBy(s => s.DayOfWeek)
                    .Select(d => new
                    {
                        Day = d.Key,
                        Lessons = d.Select(l => new
                        {
                            Number = l.Number,
                            Id = l.LessonId,
                        })
                        .OrderBy(l => l.Number)
                    })
                    .OrderBy(d=>d.Day)
            })
            .OrderBy(s=>s.Grade)
            .ToList();
        List<GradeSchedule> gradeSchedules = [];
        foreach (var scheduleItem in schedulesCollection)
        {
            List<DayLesson> dayLessons = [];
            foreach (var schedule in scheduleItem.Schedule)
            {
                List<LessonNumber> lessonNumbers = [];
                foreach (var lesson in schedule.Lessons)
                {
                    lessonNumbers.Add(new LessonNumber(lesson.Number, lesson.Id));
                }
                dayLessons.Add( new DayLesson(schedule.Day, ServiceHelper.DayOfWeekToString(schedule.Day), lessonNumbers));
            }
            gradeSchedules.Add(new GradeSchedule(scheduleItem.Grade, scheduleItem.GradeLevelId, dayLessons));
        }
        return gradeSchedules;
    }

    public async Task<Schedule> Update(Schedule schedule)
    {
        return await _scheduleStore.Update(schedule.Id, schedule);
    }

    public async Task Create(Schedule schedule)
    {
        await _scheduleStore.Add(schedule);
    }

    public async Task InsertOrUpdateSchedule(List<Schedule> schedules)
    {
        foreach (var item in schedules)
        {
            var findSchedule = await _scheduleStore.GetByGradeLevelAndDay(item.GradeLevelId, item.DayOfWeek, item.Number);
            if (findSchedule != null)
            {
                item.Id = findSchedule.Id;
                await _scheduleStore.Update(findSchedule.Id, item);
            }
            else
            {
                await _scheduleStore.Add(item);
            }
        }
    }

    public async Task AddScheduleCollection(List<Schedule> schedules)
    {
        await _scheduleStore.DeActivateAllSchedule();
        foreach (var item in schedules)
        {
            await _scheduleStore.Add(item);
        }
    }
}
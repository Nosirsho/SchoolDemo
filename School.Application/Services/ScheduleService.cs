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

    public async Task<IReadOnlyList<GradeSchedule>> GetAll()
    {
        var schedules = await _scheduleStore.GetAll();
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
                    })
            })
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
        return await _scheduleStore.Update(schedule);
    }

    public async Task Create(Schedule schedule)
    {
        await _scheduleStore.Add(schedule);
    }
}
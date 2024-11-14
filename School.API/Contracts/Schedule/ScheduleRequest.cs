namespace School.API.Contracts.Schedule;

public record ScheduleRequest(Guid GradeLevelId, List<DayLessonRequest> DayLessons);
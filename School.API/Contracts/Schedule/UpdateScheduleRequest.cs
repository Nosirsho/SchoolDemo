namespace School.API.Contracts.Schedule;

public record UpdateScheduleRequest(
    Guid Id,
    DayOfWeek DayOfWeek, 
    Guid LessonId, 
    Guid TeacherId, 
    Guid GradeLevelId
);
namespace School.API.Contracts.Schedule;

public record CreateScheduleRequest(
    DayOfWeek DayOfWeek, 
    Guid LessonId, 
    Guid TeacherId, 
    Guid GradeLevelId
    );
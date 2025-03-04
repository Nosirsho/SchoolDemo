namespace School.API.Contracts.Schedule;

public record DayLessonRequest(int DayInt, List<LessonNumberRequest> LessonNumbers);
namespace School.API.Contracts.Schedule;

public record GetScheduleResponse(
    IReadOnlyList<ScheduleResponseList> ScheduleResponseList
);

public record ScheduleResponseList(string GradeLevel, List<ScheduleItem> ScheduleItems);
public record ScheduleItem(DayOfWeek Day, List<Lesson> Lessons);
public record Lesson(int Number, string Name);
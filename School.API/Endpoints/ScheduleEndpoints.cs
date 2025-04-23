using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Parent;
using School.API.Contracts.Schedule;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class ScheduleEndpoints
{
    public static IEndpointRouteBuilder MapScheduleEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("schedules");
        endpoints.MapGet("{gradeLevelId:guid}/{date:datetime}", GetScheduleByDayForGradeLevel);
        //endpoints.MapPost("bind/{id:guid}", BindParentStudents);
        
        return endpoints;
    }
    
    private static async Task<IResult> GetScheduleByDayForGradeLevel(
        [FromRoute] Guid gradeLevelId,
        [FromRoute] DateTime date,
        ScheduleService service )
    {
        var schedules = await service.GetScheduleByDayForGradeLevel(gradeLevelId, date);
        return Results.Ok(new ApiResponse<IEnumerable<GetLessonForDay>>(schedules.Select(sh=> new GetLessonForDay(sh.LessonId, sh.Lesson.Name, sh.Number))));
    }
}
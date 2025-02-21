using Microsoft.AspNetCore.Mvc;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class GradeBookEndpoints
{
    public static IEndpointRouteBuilder MapGradeBookEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("gradebook");
        endpoints.MapGet(string.Empty, GetGradeBooks);
        endpoints.MapGet("/{start:datetime}/{end:datetime}", GetIntervalGradeBooks);
        
        return endpoints;
    }

    private static async Task<IResult> GetGradeBooks(HttpContext context, GradeBookService servise)
    {
        var result = await servise.GetStudentGrades();
        
        return Results.Ok(new ApiResponse<IEnumerable<StudentGradeBook>>(result));
    }
    private static async Task<IResult> GetIntervalGradeBooks(HttpContext context,
        [FromRoute] DateTime start,
        [FromRoute] DateTime end,
        GradeBookService servise)
    {
        var result = await servise.GetIntervalStudentGrades(start, end);
        
        return Results.Ok(new ApiResponse<IEnumerable<StudentGradeBook>>(result));
    }
}
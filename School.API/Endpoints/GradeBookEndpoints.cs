using School.Application.Services;

namespace School.API.Endpoints;

public static class GradeBookEndpoints
{
    public static IEndpointRouteBuilder MapGradeBookEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("gradebook");
        endpoints.MapGet(string.Empty, GetGradeBooks);
        
        return endpoints;
    }

    private static async Task<IResult> GetGradeBooks(HttpContext context, GradeBookService servise)
    {
        var result = await servise.GetStudentGrades();
        return Results.Ok(result);
    }
}
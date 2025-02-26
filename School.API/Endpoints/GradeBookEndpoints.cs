using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeBook;
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
        endpoints.MapGet("/{lessonId:guid}/{start:datetime}/{end:datetime}", GetByLessonIntervalGradeBooks);
        endpoints.MapPost(string.Empty, CreateGradeBook);
        app.MapDelete("/{id:guid}", DeleteCurrentDayGradeBook);
        
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
    private static async Task<IResult> GetByLessonIntervalGradeBooks(HttpContext context,
        [FromRoute] Guid lessonId,
        [FromRoute] DateTime start,
        [FromRoute] DateTime end,
        GradeBookService servise)
    {
        var result = await servise.GetByLessonIntervalStudentGrades(start, end, lessonId);
        
        return Results.Ok(new ApiResponse<IEnumerable<StudentGradeBook>>(result));
    }
    
    private static async Task<IResult> CreateGradeBook(
        [FromBody] CreateGradeBookRequest request,
        IValidator<CreateGradeBookRequest> validator,
        GradeBookService service)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
                return Results.Ok(new ApiResponse<object>(0, errors));
            }

            var gradeBook = GradeBook.Create(
                new Guid(),
                request.Date.ToUniversalTime(),
                request.LessonId,
                new Guid("bb748ca0-1b09-4d8b-ab5b-62177dad6a76"),
                request.StudentId,
                request.Grade,
                ""
            );
            await service.Create(gradeBook);
            return Results.Ok(new ApiResponse<GradeBook>(gradeBook, 1, "Grade book created"));
        }
        catch (Exception e)
        {
            return Results.Ok(new ApiResponse<object>( 0, e.Message));
        }
    }

    private static async Task<IResult> DeleteCurrentDayGradeBook(
        [FromRoute] Guid id,
        GradeBookService servise
        )
    {
        await servise.Delete(id);
        return Results.Ok(new ApiResponse<Guid>(id, 1, "Grade book deleted"));
    }
}
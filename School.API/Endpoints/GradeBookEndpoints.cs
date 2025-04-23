using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeBook;
using School.Application.Services;
using School.Core.Model;
using School.Core.Model.GradeBookDto;

namespace School.API.Endpoints;

public static class GradeBookEndpoints
{
    public static IEndpointRouteBuilder MapGradeBookEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("gradebook");
        endpoints.MapGet(string.Empty, GetGradeBooks);
        endpoints.MapGet("/{start:datetime}/{end:datetime}", GetIntervalGradeBooks);
        endpoints.MapGet("/{lessonId:guid}/{gradeLevelId:guid}/{start:datetime}/{end:datetime}", GetByLessonIntervalGradeBooks);
        endpoints.MapGet("/{studentId:guid}/{date:datetime}", GetStudentLessonsGrade);
        endpoints.MapGet("/{date:datetime}", GetStudentsLessonsGrade);
        endpoints.MapPost(string.Empty, CreateGradeBook);
        endpoints.MapDelete(string.Empty, DeleteCurrentDayGradeBook);
        
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
        GradeBookService service)
    {
        var result = await service.GetIntervalStudentGrades(start, end);
        
        return Results.Ok(new ApiResponse<IEnumerable<StudentGradeBook>>(result));
    }
    private static async Task<IResult> GetByLessonIntervalGradeBooks(HttpContext context,
        [FromRoute] Guid lessonId,
        [FromRoute] Guid gradeLevelId,
        [FromRoute] DateTime start,
        [FromRoute] DateTime end,
        GradeBookService service)
    {
        var result = await service.GetByLessonIntervalStudentGrades(start, end, lessonId, gradeLevelId);
        
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
                new Guid("1575786b-c1a9-4435-93d2-6de9c02622ad"),
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

    private static async Task<IResult> GetStudentLessonsGrade(
        [FromRoute] Guid studentId,
        [FromRoute] DateTime date,
        GradeBookService service
        )
    {
        var result = await service.GetStudentLessonsGrade(studentId, date);
        return Results.Ok(new ApiResponse<IEnumerable<StudentLessonGradeDto>>(result));
    }

    private static async Task<IResult> DeleteCurrentDayGradeBook(
        [FromBody] DeleteGradeBookRequest request,
        GradeBookService service
        )
    {
        try
        {
            var result =  await service.Delete(request.StudentId, request.LessonId, request.Date);
            return Results.Ok(new ApiResponse<Guid>(result, 1, "Grade book deleted"));
        }
        catch (Exception e)
        {
            return Results.Ok(new ApiResponse<object>(0, e.Message));
        }
    }
    private static async Task<IResult> GetStudentsLessonsGrade(
        [FromRoute] DateTime date,
        GradeBookService service
    )
    {
        var result = await service.GetStudentLessonsGrade(null, date);
        return Results.Ok(new ApiResponse<IEnumerable<StudentLessonGradeDto>>(result));
    }
        
}
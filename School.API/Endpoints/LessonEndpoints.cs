using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeLevel;
using School.API.Contracts.Lesson;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class LessonEndpoints
{
    public static IEndpointRouteBuilder MapLessonEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("lessons");
        endpoints.MapGet(string.Empty, GetLessons);
        endpoints.MapGet("/{id:guid}", GetLessonById);
        endpoints.MapPost(string.Empty, CreateLesson);
        endpoints.MapPut("/{id:guid}", UpdateLesson);
        endpoints.MapDelete("{id:guid}", DeleteLesson);
        // endpoints.MapGet("year", GetGradeLevelsWithYear);
        // endpoints.MapGet("year/{id:guid}", GetGradeLevelsWithYearById);
        
        
        return endpoints;
    }
    
    private static async Task<IResult> GetLessons(
        LessonService service
    )
    {
        var lessons = await service.GetAll();
        var result = lessons.Select(l=>  new GetLessonResponse(l.Id, l.Name));
        return Results.Ok(new ApiResponse<IEnumerable<GetLessonResponse>>(result));
    }

    private static async Task<IResult> GetLessonById(
        [FromRoute] Guid id,
        LessonService service
        )
    {
        var lesson = await service.GetById(id);
        if (lesson == null)
        {
            return Results.Ok(new ApiResponse<object>(0, "Lesson not found"));
        }
        var response = new GetGradeLevelResponse(lesson.Id, lesson.Name);
        return Results.Ok( new ApiResponse<GetGradeLevelResponse>(response));
    }
    private static async Task<IResult> CreateLesson(
        [FromBody] CreateLessonRequest request,
        IValidator<CreateLessonRequest> validator,
        LessonService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.Ok( new ApiResponse<object>(0, errors));
        }

        var lesson = Lesson.Create(
            Guid.NewGuid(),
            request.Name
        );
        var curLesson =  await service.Create(lesson);
        var response = new GetLessonResponse(curLesson.Id, curLesson.Name);
        return Results.Ok( new ApiResponse<GetLessonResponse>(response));
    }
    
    private static async Task<IResult> UpdateLesson(
        [FromRoute] Guid id,
        [FromBody] UpdateLessonRequest request,
        IValidator<UpdateLessonRequest> validator,
        LessonService service
    )
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid || id == Guid.Empty || request.Id != id)
        {
            string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            if (id == Guid.Empty)
            {
                errors += " Id is empty";
            }
            if (request.Id != id)
            {
                errors += " request.Id != id";
            }
            return Results.Ok( new ApiResponse<object>(0, errors));
        }
        
        var lesson = Lesson.Create(
            request.Id,
            request.Name);
        var curGradeLevel = await service.Update(lesson);
        var response = new GetLessonResponse(curGradeLevel.Id, curGradeLevel.Name);
        return Results.Ok( new ApiResponse<GetLessonResponse>(response));
    }
    
    private static async Task<IResult> DeleteLesson(
        [FromRoute] Guid id,
        LessonService service
    )
    {
        try
        {
            var result =  await service.Delete(id);
            return Results.Ok(new ApiResponse<Guid>(result, 1, "Lesson deleted"));
        }
        catch (Exception e)
        {
            return Results.Ok(new ApiResponse<object>(0, e.Message));
        }
    }
}
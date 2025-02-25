using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeLevel;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class GradeLevelEndpoints
{
    public static IEndpointRouteBuilder MapGradeLevelEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("gradelevels");
        endpoints.MapGet("/{id:guid}", GetGradeLevelById);
        endpoints.MapPost(string.Empty, CreateGradeLevel);
        endpoints.MapPut("/{id:guid}", UpdateGradeLevel);
        endpoints.MapGet(string.Empty, GetGradeLevels);
        
        return endpoints;
    }

    private static async Task<IResult> GetGradeLevelById(
        [FromRoute] Guid id,
        GradeLevelService service,
        IMapper mapper
        )
    {
        var gradeLevel = await service.GetById(id);
        if (gradeLevel == null)
        {
            return Results.Ok(new ApiResponse<object>(0, "Grade Level not found"));
        }

        var response = new GetGradeLevelResponse(gradeLevel.Id, gradeLevel.Name);
        return Results.Ok( new ApiResponse<GetGradeLevelResponse>(response));
    }

    private static async Task<IResult> CreateGradeLevel(
        [FromBody] CreateGradeLevelRequest request,
        IValidator<CreateGradeLevelRequest> createGradeLevelValidator,
        GradeLevelService service)
    {
        var validationResult = await createGradeLevelValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.Ok( new ApiResponse<object>(0, errors));
        }

        var gradeLevel = GradeLevel.Create(
            request.Name
        );
        await service.Create(gradeLevel);
        var response = new GetGradeLevelResponse(gradeLevel.Id, gradeLevel.Name);
        return Results.Ok( new ApiResponse<GetGradeLevelResponse>(response));
    }

    private static async Task<IResult> UpdateGradeLevel(
        [FromRoute] Guid id,
        [FromBody] UpdateGradeLevelRequest request,
        IValidator<UpdateGradeLevelRequest> validator,
        GradeLevelService service
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
        
        var gradeLevel = GradeLevel.Create(
            request.Id,
            request.Name,
            request.EntryDate
        );
        var curGradeLevel = await service.Update(gradeLevel);
        var response = new GetGradeLevelResponse(curGradeLevel.Id, curGradeLevel.Name);
        return Results.Ok( new ApiResponse<GetGradeLevelResponse>(response));
    }

    private static async Task<IResult> GetGradeLevels(
        GradeLevelService service
        )
    {
        var gradeLevels = await service.GetAll();
        var result = gradeLevels.Select(gl=>  new GetGradeLevelResponse(gl.Id, gl.Name));
        return Results.Ok(new ApiResponse<IEnumerable<GetGradeLevelResponse>>(result));
    }
}
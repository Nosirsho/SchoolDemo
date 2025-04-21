using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Parent;
using School.API.Contracts.Teacher;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class TeacherEndpoints
{
    public static IEndpointRouteBuilder MapTeacherEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("teachers");
        endpoints.MapGet(string.Empty, GetTeachers);
        endpoints.MapGet("{id:guid}", GetTeacherById);
        endpoints.MapPut("{id:guid}", UpdateTeacher);
        endpoints.MapPost(string.Empty, CreateTeacher);
        endpoints.MapDelete("{id:guid}", DeleteTeacher);
        return endpoints;
    }

    private static async Task<IResult> GetTeachers(HttpContext context, TeacherService service)
    {
        var teachers = await service.GetAll();
        var result = teachers.Select(t=> 
            new GetTeachersListResponse(
                t.Id, 
                string.Join(" ", t.LastName, t.FirstName, t.MiddleName),
                DateOnly.FromDateTime(t.BirthDate),
                t.Phone,
                HelperService.GetSexText(t.Sex)
            ));
        return Results.Ok(new ApiResponse<IEnumerable<GetTeachersListResponse>>(result));
    }
    
    private static async Task<IResult> GetTeacherById(
        [FromRoute] Guid id,
        TeacherService service
    )
    {
        var teacher = await service.GetById(id);
        if (teacher == null)
        {
            return Results.Ok(new ApiResponse<object>(0, "Teacher not found"));
        }
        var result = new GetTeacherByIdResponse(teacher.Id, teacher.FirstName, teacher.LastName, teacher.MiddleName, DateOnly.FromDateTime(teacher.BirthDate), ((int)teacher.Sex).ToString(), teacher.Phone);

        return Results.Ok(new ApiResponse<GetTeacherByIdResponse>(result));
    }
    
    private static async Task<IResult> UpdateTeacher(
        [FromRoute] Guid id,
        [FromBody] UpdateTeacherRequest request,
        IValidator<UpdateTeacherRequest> validator,
        TeacherService service
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
        var teacher = Teacher.Create(
            request.Id, 
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Phone,
            request.BirthDate,
            request.Sex
        );
        var curTeacher = await service.Update(teacher);
        var fullName = $"{curTeacher.LastName} {curTeacher.FirstName[0]}. {curTeacher.MiddleName[0]}.";
        var result = new GetTeachersListResponse(curTeacher.Id, 
            fullName,
            DateOnly.FromDateTime(curTeacher.BirthDate),
            curTeacher.Phone,
            HelperService.GetSexText(curTeacher.Sex));
        
        return Results.Ok( new ApiResponse<GetTeachersListResponse>(result));
    }
    
    private static async Task<IResult> CreateTeacher(
        [FromBody] CreateTeacherRequest request,
        IValidator<CreateTeacherRequest> validator,
        TeacherService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.Ok( new ApiResponse<object>(0, errors));
        }
        var teacher = Teacher.Create(
            Guid.NewGuid(), 
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Phone,
            request.BirthDate,
            request.Sex
        );
        
        var curTeacher  = await service.Create(teacher);
        var fullName = string.Format("{0} {1}. {2}.",curTeacher.LastName, curTeacher.FirstName[0], curTeacher.MiddleName[0]);
        var result = new GetTeachersListResponse(curTeacher.Id, 
            fullName,
            DateOnly.FromDateTime(curTeacher.BirthDate),
            curTeacher.Phone,
            HelperService.GetSexText(curTeacher.Sex));
        return Results.Ok( new ApiResponse<GetTeachersListResponse>(result));
    }
    
    private static async Task<IResult> DeleteTeacher(
        [FromRoute] Guid id,
        TeacherService servise
    )
    {
        try
        {
            var result =  await servise.Delete(id);
            return Results.Ok(new ApiResponse<Guid>(result, 1, "Teachers deleted"));
        }
        catch (Exception e)
        {
            return Results.Ok(new ApiResponse<object>(0, e.Message));
        }
    }
}
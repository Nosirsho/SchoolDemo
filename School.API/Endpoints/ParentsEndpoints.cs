using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Parent;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class ParentsEndpoints
{
    public static IEndpointRouteBuilder MapParentsEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("parents");
        endpoints.MapGet("/{id:guid}", GetPaerentById);
        endpoints.MapGet("/bind/{id:guid}", GetPaerentWhithStudents);
        endpoints.MapPost(string.Empty, CreateParent);
        endpoints.MapPost("bind/{id:guid}", BindParentStudents);
        endpoints.MapPut("/{id:guid}", UpdateParent);
        endpoints.MapGet(string.Empty, GetParents);
        //endpoints.MapPost("/", UpdateParent);
        return endpoints;
    }

    private static async Task<IResult> CreateParent(
        [FromBody] CreateParentRequest request,
        IValidator<CreateParentRequest> validator,
        ParentService service)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            string? errors = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
            return Results.Ok( new ApiResponse<object>(0, errors));
        }
        var parent = Parent.Create(
            new Guid(), 
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Sex,
            request.Phone
            );
        var curParent  = await service.AddParentWithStudent(parent, request.StudentId);
        var fullName = string.Format("{0} {1}. {2}.",curParent.LastName, curParent.FirstName[0], curParent.MiddleName[0]);
        var result = new GetParentResponse(curParent.Id, fullName, ((int)curParent.Sex).ToString(), curParent.Phone);
        return Results.Ok( new ApiResponse<GetParentResponse>(result));
    }

    private static async Task<IResult> GetPaerentById(
        [FromRoute] Guid id,
        ParentService service
        )
    {
        var parent = await service.GetById(id);
        if (parent == null)
        {
            return Results.Ok(new ApiResponse<object>(0, "Parent not found"));
        }
        var result = new GetParentByIdResponse(parent.Id, parent.FirstName, parent.LastName, parent.MiddleName, ((int)parent.Sex).ToString(), parent.Phone);

        return Results.Ok(new ApiResponse<GetParentByIdResponse>(result));
    }
    
    private static async Task<IResult> GetPaerentWhithStudents(
        [FromRoute] Guid id,
        ParentService service
    )
    {
        var parent = await service.GetById(id);
        if (parent == null)
        {
            return Results.Ok(new ApiResponse<object>(0, "Parent not found"));
        }
        var fullName = $"{parent.LastName} {parent.FirstName} {parent.MiddleName}";

        var parentResp =  new GetParentWithStudentResponse(parent.Id, fullName,[]);
        foreach (var child in parent.Students)
        {
            var childFullName = $"{child.LastName} {child.FirstName} {child.MiddleName}";
            parentResp.Children.Add( new Child(child.Id, childFullName));
        }
        return Results.Ok(new ApiResponse<GetParentWithStudentResponse>(parentResp));
    }
    
    private static async Task<IResult> UpdateParent(
        [FromRoute] Guid id,
        [FromBody] UpdateParentRequest request,
        IValidator<UpdateParentRequest> validator,
        ParentService service
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
        
        var parent = Parent.Create(
            request.Id, 
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Sex,
            request.Phone
        );
        var curParent = await service.Update(parent);
        var fullName = $"{curParent.LastName} {curParent.FirstName[0]}. {curParent.MiddleName[0]}.";
        var result = new GetParentResponse(parent.Id, fullName, ((int)curParent.Sex).ToString(), curParent.Phone);
        return Results.Ok( new ApiResponse<GetParentResponse>(result));
    }

    private static async Task<IResult> GetParents(ParentService service)
    {
        var parents = await service.GetAll();
        var result = parents.Select(gl=>  
            new GetParentResponse(
                gl.Id, 
                string.Format("{0} {1}. {2}.",gl.LastName, gl.FirstName[0], gl.MiddleName[0]),
                ((int)gl.Sex).ToString(), 
                gl.Phone)
        );
        return Results.Ok(new ApiResponse<IEnumerable<GetParentResponse>>(result));
    }

    private static async Task<IResult> BindParentStudents(
        [FromRoute] Guid id,
        [FromBody] List<Guid> studentIds,
        ParentService service
        )
    {
        await service.BindingParentStudents(id,studentIds);
        return Results.Ok(new ApiResponse<object>("",1, "All students binding"));
    }
}
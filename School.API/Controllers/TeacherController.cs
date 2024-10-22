using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Teacher;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Controllers;
[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    private readonly TeacherService _teacherService;
    private readonly IValidator<CreateTeacherRequest> _createTeacherValidator;
    private readonly IValidator<UpdateTeacherRequest> _updateTeacherValidator;


    public TeacherController(TeacherService teacherService, IValidator<CreateTeacherRequest> createTeacherValidator, IValidator<UpdateTeacherRequest> updateTeacherValidator)
    {
        _teacherService = teacherService;
        _createTeacherValidator = createTeacherValidator;
        _updateTeacherValidator = updateTeacherValidator;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Teacher>> Get(Guid id)
    {
        var teacher = await _teacherService.GetById(id);
        return Ok(teacher);
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Teacher>>> GetAll()
    {
        var teachers = await _teacherService.GetAll();
        var result = teachers.Select(t=> 
            new GetTeachersListResponse(
                t.Id, 
                string.Join(" ", t.LastName, t.FirstName, t.MiddleName),
                DateOnly.FromDateTime(t.BirthDate),
                t.Phone,
                HelperService.GetSexText(t.Sex)
                ));
        return Ok(result);
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<Teacher>>> GetAll(string? search)
    {
        var teachers = await _teacherService.Search(search);
        var result = teachers.Select(t=> 
            new GetTeachersListResponse(
                t.Id, 
                string.Join(" ", t.LastName, t.FirstName, t.MiddleName),
                DateOnly.FromDateTime(t.BirthDate),
                t.Phone,
                HelperService.GetSexText(t.Sex)
            ));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateTeacherRequest request)
    {
        var validationResult = await _createTeacherValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var teacher = new Teacher(
            request.FirstName, 
            request.MiddleName, 
            request.LastName,
            request.Phone,
            request.BirthDate,request.Sex);
        await _teacherService.Create(teacher);
        var result = new GetTeachersListResponse(
            teacher.Id, 
            string.Join(" ", teacher.LastName, teacher.FirstName, teacher.MiddleName),
            DateOnly.FromDateTime(teacher.BirthDate),
            teacher.Phone,
            HelperService.GetSexText(teacher.Sex)
            );
        
        return Ok(result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Teacher>> Update(Guid id, UpdateTeacherRequest request)
    {
        var validationResult = await _updateTeacherValidator.ValidateAsync(request);
        if (!validationResult.IsValid || id == Guid.Empty || request.Id != id)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var teacher = new Teacher(
            request.Id,
            request.FirstName, 
            request.MiddleName, 
            request.LastName, 
            request.Phone,
            request.BirthDate,
            request.Sex
        );
        var curParent = await _teacherService.Update(teacher);
        var result = new GetTeachersListResponse(
            curParent.Id, 
            string.Join(" ", curParent.LastName, curParent.FirstName, curParent.MiddleName),
            DateOnly.FromDateTime(curParent.BirthDate),
            curParent.Phone,
            HelperService.GetSexText(curParent.Sex)
        );
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _teacherService.Delete(id);
        return Ok(result);
    }


}
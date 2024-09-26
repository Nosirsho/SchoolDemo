using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Teacher;
using School.API.Validations.Teacher;
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
        return Ok(teachers);
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
        return Ok();
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
        return Ok(curParent);
    }
    
    
}
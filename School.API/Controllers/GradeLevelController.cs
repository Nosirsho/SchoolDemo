using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeLevel;
using School.API.Contracts.Teacher;
using School.API.Validations.Teacher;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Controllers;
[ApiController]
[Route("[controller]")]
public class GradeLevelController : ControllerBase
{
    private readonly GradeLevelService _gradeLevelService;
    private readonly IValidator<CreateGradeLevelRequest> _createGradeLevelValidator;
    private readonly IValidator<UpdateGradeLevelRequest> _updateGradeLevelValidator;

    public GradeLevelController(GradeLevelService gradeLevelService, IValidator<CreateGradeLevelRequest> createGradeLevelValidator, IValidator<UpdateGradeLevelRequest> updateGradeLevelValidator)
    {
        _gradeLevelService = gradeLevelService;
        _createGradeLevelValidator = createGradeLevelValidator;
        _updateGradeLevelValidator = updateGradeLevelValidator;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GradeLevel>> Get(Guid id)
    {
        var gradeLevel = await _gradeLevelService.GetById(id);
        return Ok(gradeLevel);
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Teacher>>> GetAll()
    {
        var gradeLevels = await _gradeLevelService.GetAll();
        return Ok(gradeLevels);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateGradeLevelRequest request)
    {
        var validationResult = await _createGradeLevelValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var gradeLevel = new GradeLevel(
                request.Name
            );
        await _gradeLevelService.Create(gradeLevel);
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GradeLevel>> Update(Guid id, UpdateGradeLevelRequest request)
    {
        var validationResult = await _updateGradeLevelValidator.ValidateAsync(request);
        if (!validationResult.IsValid || id == Guid.Empty || request.Id != id)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var gradeLevel = new GradeLevel(
            request.Id,
            request.Name,
            request.EntryDate
        );
        var curGradeLevel = await _gradeLevelService.Update(gradeLevel);
        return Ok(curGradeLevel);
    }
}
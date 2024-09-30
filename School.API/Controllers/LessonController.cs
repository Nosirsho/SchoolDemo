using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Lesson;
using School.API.Validations.Lesson;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Controllers;
[ApiController]
[Route("[controller]")]
public class LessonController : ControllerBase
{
    private readonly LessonService _lessonService;
    private readonly IValidator<CreateLessonRequest> _createLessonValidator;
    private readonly IValidator<UpdateLessonRequest> _updateLessonValidator;

    public LessonController(LessonService lessonService, 
        IValidator<CreateLessonRequest>  createLessonValidator, 
        IValidator<UpdateLessonRequest> updateLessonValidator)
    {
        _lessonService = lessonService;
        _createLessonValidator = createLessonValidator;
        _updateLessonValidator = updateLessonValidator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Lesson>> Get(Guid id)
    {
        return Ok(await _lessonService.GetById(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Lesson>>> GetAll()
    {
        return Ok(await _lessonService.GetAll());
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateLessonRequest request)
    {
        var validationResult = await _createLessonValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var lesson = new Lesson(
            request.Name
        );
        await _lessonService.Create(lesson);
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Lesson>> Update(Guid id, UpdateLessonRequest request)
    {
        var validationResult = await _updateLessonValidator.ValidateAsync(request);
        if (!validationResult.IsValid || id == Guid.Empty || request.Id != id)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var lesson = new Lesson(
            request.Id,
            request.Name
        );
        var curlesson = await _lessonService.Update(lesson);
        return Ok(curlesson);
    }
    
}
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Schedule;
using School.API.Validations;
using School.Application.Services;
using School.Core.Model;
using School.Persistence;

namespace School.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _scheduleService;
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IValidator<CreateScheduleRequest> _createScheduleValidator;
    private readonly IValidator<UpdateScheduleRequest> _updateScheduleValidator;

    public ScheduleController(ScheduleService scheduleService,
        SchoolDbContext schoolDbContext,
        IValidator<CreateScheduleRequest> createScheduleValidator,
        IValidator<UpdateScheduleRequest> updateScheduleValidator)
    {
        _scheduleService = scheduleService;
        _schoolDbContext = schoolDbContext;
        _createScheduleValidator = createScheduleValidator;
        _updateScheduleValidator = updateScheduleValidator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Schedule>> Get(Guid id)
    {
        return Ok(await _scheduleService.GetById(id));
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GradeSchedule>>> GetAll()
    {
        return Ok(await _scheduleService.GetAll());
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateScheduleRequest request)
    {
        var validationResult = await _createScheduleValidator.ValidateAsync(request);
        var lesson = await _schoolDbContext.Lessons.FindAsync(request.LessonId);
        var teacher = await _schoolDbContext.Teachers.FindAsync(request.TeacherId);
        var gradelevel = await _schoolDbContext.GradeLevels.FindAsync(request.GradeLevelId);
        if (!validationResult.IsValid || lesson is null || teacher is null || gradelevel is null)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var gradeLevel = new Schedule(
            request.DayOfWeek,
            lesson,
            teacher,
            gradelevel
            );
        await _scheduleService.Create(gradeLevel);
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GradeLevel>> Update(Guid id, UpdateScheduleRequest request)
    {
        var validationResult = await _updateScheduleValidator.ValidateAsync(request);
        var lesson = await _schoolDbContext.Lessons.FindAsync(request.LessonId);
        var teacher = await _schoolDbContext.Teachers.FindAsync(request.TeacherId);
        var gradeLevel = await _schoolDbContext.GradeLevels.FindAsync(request.GradeLevelId);
        if (!validationResult.IsValid || id == Guid.Empty || request.Id != id 
            || lesson is null || teacher is null || gradeLevel is null)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var schedule = new Schedule(
            request.Id,
            request.DayOfWeek,
            lesson,
            teacher,
            gradeLevel
        );
        var curSchedule = await _scheduleService.Update(schedule);
        return Ok(curSchedule);
    }

}
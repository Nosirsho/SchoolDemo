using FluentValidation;
using FluentValidation.Results;
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
    private readonly IValidator<ScheduleRequest> _scheduleRequestValidator;
    private readonly IValidator<UpdateScheduleRequest> _updateScheduleValidator;

    public ScheduleController(ScheduleService scheduleService,
        SchoolDbContext schoolDbContext,
        IValidator<ScheduleRequest> scheduleRequestValidator,
        IValidator<UpdateScheduleRequest> updateScheduleValidator)
    {
        _scheduleService = scheduleService;
        _schoolDbContext = schoolDbContext;
        _scheduleRequestValidator = scheduleRequestValidator;
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
    public async Task<ActionResult> CreateUpdateSchedule(List<ScheduleRequest> request)
    {
        var validationResults = new List<ValidationResult>();
        foreach (var requestItem in request)
        {
            var result = await _scheduleRequestValidator.ValidateAsync(requestItem);
            validationResults.Add(result);
        }

        if (validationResults.Any(r => !r.IsValid))
        {
            return BadRequest(validationResults.SelectMany(r => r.Errors));
        }
        // var res = request.Select( s => new {
        //     GradeLevelId = s.GradeLevelId, 
        //     DayLessons = s.DayLessons.Select(
        //     d => new 
        //     {
        //         dayInt = (DayOfWeek)d.DayInt,
        //         LessonNumbers = d.LessonNumbers.Select(l => new 
        //         {
        //             Number = l.Number,
        //             LessonId = l.LessonId
        //         })
        //     })}).ToList();
        var schedules = request
            .SelectMany(r => r.DayLessons.SelectMany(d => d.LessonNumbers.Select(l 
                => new Schedule(
                    r.GradeLevelId,
                    (DayOfWeek)d.DayInt,
                    l.LessonId,
                    l.Number
                )
            ))).ToList();

        //await _scheduleService.InsertOrUpdateSchedule(schedules);
        await _scheduleService.AddScheduleCollection(schedules);
        return  Ok();
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
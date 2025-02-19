using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Schedule;
using School.API.Validations;
using School.Application.Services;
using School.Core.Model;
using School.Persistence;
using Lesson = School.Core.Model.Lesson;

namespace School.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _scheduleService;
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IValidator<CreateScheduleRequest> _createScheduleValidator;
    private readonly IValidator<UpdateScheduleRequest> _updateScheduleValidator;
    private readonly IMapper _mapper;

    public ScheduleController(ScheduleService scheduleService,
        SchoolDbContext schoolDbContext,
        IValidator<CreateScheduleRequest> createScheduleValidator,
        IValidator<UpdateScheduleRequest> updateScheduleValidator,
        IMapper mapper)
        
    {
        _scheduleService = scheduleService;
        _schoolDbContext = schoolDbContext;
        _createScheduleValidator = createScheduleValidator;
        _updateScheduleValidator = updateScheduleValidator;
        _mapper = mapper;
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
        var lesson = _mapper.Map<Lesson>(await _schoolDbContext.Lessons.FindAsync(request.LessonId));
        var teacher =_mapper.Map<Teacher>(await _schoolDbContext.Teachers.FindAsync(request.TeacherId));
        var gradeLevel = _mapper.Map<GradeLevel>(await _schoolDbContext.GradeLevels.FindAsync(request.GradeLevelId));
        if (!validationResult.IsValid || lesson is null || teacher is null || gradeLevel is null)
        {
            return BadRequest(validationResult.Errors);
        }
        
        var schedule = Schedule.Create(
            request.DayOfWeek,
            lesson,
            teacher,
            gradeLevel
            );
        await _scheduleService.Create(schedule);
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GradeLevel>> Update(Guid id, UpdateScheduleRequest request)
    {
        var validationResult = await _updateScheduleValidator.ValidateAsync(request);
        var lesson = _mapper.Map<Lesson>(await _schoolDbContext.Lessons.FindAsync(request.LessonId));
        var teacher =_mapper.Map<Teacher>(await _schoolDbContext.Teachers.FindAsync(request.TeacherId));
        var gradeLevel = _mapper.Map<GradeLevel>(await _schoolDbContext.GradeLevels.FindAsync(request.GradeLevelId));
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
using System.Collections.ObjectModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Student;
using School.Application.Services;
using School.Core.Model;
using School.Persistence;

namespace School.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    private readonly GradeLevelService _gradeLevelService;
    private readonly IValidator<CreateStudentRequest> _createStudentValidator;
    private readonly IValidator<UpdateStudentRequest> _updateStudentValidator;
    private readonly ILogger<StudentController> _logger;
    private readonly SchoolDbContext _schoolDbContext;

    public StudentController(
        StudentService studentService,
        GradeLevelService gradeLevelService,
        IValidator<CreateStudentRequest> createStudentValidator,
        IValidator<UpdateStudentRequest> updateStudentValidator,
        ILogger<StudentController> logger,
        SchoolDbContext schoolDbContext)
    {
        _studentService = studentService;
        _gradeLevelService = gradeLevelService;
        _createStudentValidator = createStudentValidator;
        _updateStudentValidator = updateStudentValidator;
        _logger = logger;
        _schoolDbContext = schoolDbContext;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Student>> Get(Guid id)
    {
        var student = await _studentService.GetById(id);
        var result = new GetStudentResponse(
            student.Id,
            student.FirstName,
            student.LastName,
            student.MiddleName,
            DateOnly.FromDateTime(student.BirthDate),
            student.GradeLevelId,
            ((int)student.Sex).ToString()
            );
        return Ok(result);
    }

    [HttpGet("{fullname}")]
    public async Task<ActionResult<IEnumerable<Student>>> GetByFullName(string fullname)
    {
        var students = await _studentService.GetByFullname(fullname);
        return Ok(students);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Student>>> GetAll()
    {
        _logger.LogInformation("Get all students");
        var students = await _studentService.GetAll();
        var result = students.Select(s =>
            new GetStudentsListResponse(
                s.Id, 
                string.Join(" ", s.LastName, s.FirstName, s.MiddleName), 
                DateOnly.FromDateTime(s.BirthDate), 
                s.GradeLevel?.Name,
                HelperService.GetSexFromDb(s.Sex)
                )
        );
        return Ok(result);
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<Teacher>>> Search(string? search)
    {
        var students = await _studentService.Search(search);
        var result = students.Select(s =>
            new GetStudentsListResponse(
                s.Id, 
                string.Join(" ", s.LastName, s.FirstName, s.MiddleName), 
                DateOnly.FromDateTime(s.BirthDate), 
                s.GradeLevel?.Name,
                HelperService.GetSexFromDb(s.Sex)
            )
        );
        return Ok(result);
    }
    
    [HttpGet("/grade{gradeId:guid}")]
    public async Task<ActionResult<IReadOnlyList<Student>>> GetByGrade(Guid gradeId)
    {
        _logger.LogInformation("Get all by grade");
        var students = await _studentService.GetByGrade(gradeId);
        return Ok(students);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create(CreateStudentRequest request)
    {
        
        _logger.LogWarning("Create student: " + request);
        var validateResult = await _createStudentValidator.ValidateAsync(request);
        if (!validateResult.IsValid)
        {
            _logger.LogInformation("ValidationError");
            return BadRequest(validateResult.Errors);
        }
        
        var student = new Student(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.BirthDate,
            request.Sex,
            request.GradeLevelId
            );
        
        await _studentService.Create(student);
        var studentGradeLevel = await _gradeLevelService.GetById(request.GradeLevelId);
        var result = new GetStudentsListResponse(
            student.Id,
            string.Join(" ", student.LastName, student.FirstName, student.MiddleName), 
            DateOnly.FromDateTime(student.BirthDate),
            studentGradeLevel?.Name,
            HelperService.GetSexFromDb(student.Sex)
        );
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Student>> Update(Guid id, UpdateStudentRequest request)
    {
        _logger.LogDebug("Update student: Id: "+ id.ToString() + "; request: " + request);
        var validateResult = await _updateStudentValidator.ValidateAsync(request);
        if (!validateResult.IsValid)
        {
            _logger.LogInformation("ValidationError");
            return BadRequest(validateResult.Errors);
        }

        var student = new Student(
            request.Id,
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.BirthDate,
            request.Sex,
            request.GradeLevelId
        );
        student = await _studentService.Update(student);
        var studentGradeLevel = await _gradeLevelService.GetById(request.GradeLevelId);
        var result = new GetStudentsListResponse(
            student.Id,
            string.Join(" ", student.LastName, student.FirstName, student.MiddleName), 
            DateOnly.FromDateTime(student.BirthDate),
            studentGradeLevel?.Name,
            HelperService.GetSexFromDb(student.Sex)
        );
        _logger.LogDebug("Update student, Response: " + result);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _studentService.Delete(id);
        return Ok(result);
    }
}
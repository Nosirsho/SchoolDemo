using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class StudentRepository : IStudentStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly ILogger<StudentRepository> _logger;
    private readonly IMapper _mapper;

    public StudentRepository(SchoolDbContext schoolDbContext, ILogger<StudentRepository> logger, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<Student> GetById(Guid id)
    {
        var student = await _schoolDbContext.Students.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        if (student == null) throw new NullReferenceException($"Student not found with id {id}");
        return _mapper.Map<Student>(student);
    }

    public async Task<IReadOnlyList<Student>> GetByFullname(string fullname)
    {
        var st = await _schoolDbContext.Students
            .Where(s=> (s.LastName + " " + s.FirstName + " " + s.MiddleName).ToLower() == fullname.ToLower()
                && !s.IsDeleted)
            .ToListAsync();
        return _mapper.Map<IReadOnlyList<Student>>(st) ;
    }

    public async Task<IReadOnlyList<Student>> Search(string text)
    {
        var result = await _schoolDbContext.Students
            .Where(s=> (s.LastName + " " + s.FirstName + " " + s.MiddleName).ToLower()
                .Contains(text.ToLower()) && !s.IsDeleted).ToListAsync();
        return _mapper.Map<IReadOnlyList<Student>>(result);
    }

    public async Task<IReadOnlyList<Student>> GetAll()
    {
        _logger.LogInformation("Get all students");
        return _mapper.Map<IReadOnlyList<Student>>(await _schoolDbContext.Students.Include(s => s.GradeLevel).Where(s=>!s.IsDeleted) .ToListAsync());
    }

    public async Task<IReadOnlyList<Student>> GetByGrade(Guid gradeId)
    {
        return _mapper.Map<IReadOnlyList<Student>>( _schoolDbContext.Students.Where(s=>s.GradeLevelId == gradeId && !s.IsDeleted).ToList());
    }

    public async Task<Student> Update(Student student)
    {
        var curStudent = await GetById(student.Id);
        if (curStudent == null) throw new Exception("Student not found " + student.Id);
        curStudent.FirstName = student.FirstName;
        curStudent.LastName = student.LastName;
        curStudent.MiddleName = student.MiddleName;
        curStudent.BirthDate = student.BirthDate.ToUniversalTime();
        curStudent.GradeLevelId = student.GradeLevelId;
        curStudent.Sex = student.Sex;

        await _schoolDbContext.SaveChangesAsync();
        return curStudent;
    }

    public async Task Add(Student student)
    {
        var studentEntity = _mapper.Map<StudentEntity>(student);
        studentEntity.BirthDate = studentEntity.BirthDate.ToUniversalTime();
        await _schoolDbContext.Students.AddAsync(studentEntity);
        await _schoolDbContext.SaveChangesAsync();
    }

    public async Task<Guid> Delete(Guid id)
    {
        var student = await GetById(id);
        student.IsDeleted = true;
        await _schoolDbContext.SaveChangesAsync();
        return id;
    }
}
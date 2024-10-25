using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class StudentRepository : IStudentStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(SchoolDbContext schoolDbContext, ILogger<StudentRepository> logger)
    {
        _schoolDbContext = schoolDbContext;
        _logger = logger;
    }
    
    public async Task<Student> GetById(Guid id)
    {
        var student = await _schoolDbContext.Students.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        if (student == null) throw new NullReferenceException($"Student not found with id {id}");
        return student;
    }

    public async Task<IReadOnlyList<Student>> GetByFullname(string fullname)
    {
        var st = await _schoolDbContext.Students
            .Where(s=> (s.LastName + " " + s.FirstName + " " + s.MiddleName).ToLower() == fullname.ToLower()
                && !s.IsDeleted)
            .ToListAsync();
        return st;
    }

    public async Task<IReadOnlyList<Student>> Search(string text)
    {
        var result = await _schoolDbContext.Students
            .Where(s=> (s.LastName + " " + s.FirstName + " " + s.MiddleName).ToLower()
                .Contains(text.ToLower()) && !s.IsDeleted).ToListAsync();
        return result;
    }

    public async Task<IReadOnlyList<Student>> GetAll()
    {
        _logger.LogInformation("Get all students");
        return await _schoolDbContext.Students.Include(s => s.GradeLevel).Where(s=>!s.IsDeleted) .ToListAsync();
    }

    public async Task<IReadOnlyList<Student>> GetByGrade(Guid gradeId)
    {
        return _schoolDbContext.Students.Where(s=>s.GradeLevelId == gradeId && !s.IsDeleted).ToList();
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
        student.BirthDate = student.BirthDate.ToUniversalTime();
        await _schoolDbContext.Students.AddAsync(student);
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
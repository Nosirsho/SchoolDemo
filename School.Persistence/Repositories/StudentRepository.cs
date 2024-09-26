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
    
    public async Task<Student?> GetById(Guid id)
    {
        return await _schoolDbContext.Students.FindAsync(id);
    }

    public async Task<IReadOnlyList<Student>> GetByFullname(string fullname)
    {
        var st = await _schoolDbContext.Students
            .Where(x=> (x.LastName + " " + x.FirstName + " " + x.MiddleName).ToLower() == fullname.ToLower())
            .ToListAsync();
        return st;
    }

    public async Task<IReadOnlyList<Student>> GetAll()
    {
        _logger.LogInformation("Get all students");
        return await _schoolDbContext.Students.ToListAsync();
    }

    public async Task<Student> Update(Student student)
    {
        var curStudent = await _schoolDbContext.Students.FindAsync(student.Id);
        if (curStudent == null) throw new Exception("Student not found " + student.Id);
        curStudent.FirstName = student.FirstName;
        curStudent.LastName = student.LastName;
        curStudent.MiddleName = student.MiddleName;
        curStudent.BirthDate = student.BirthDate;

        await _schoolDbContext.SaveChangesAsync();
        return curStudent;
    }

    public async Task Add(Student student)
    {
        await _schoolDbContext.Students.AddAsync(student);
        await _schoolDbContext.SaveChangesAsync();
    }
}
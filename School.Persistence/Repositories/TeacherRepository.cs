using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class TeacherRepository : ITeacherStore
{
    private readonly SchoolDbContext _schoolDbContext;

    public TeacherRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    
    public async Task<Teacher?> GetById(Guid id)
    {
        return await _schoolDbContext.Teachers.FindAsync(id);
    }

    public async Task<IReadOnlyList<Teacher>> GetAll()
    {
        return await _schoolDbContext.Teachers.ToListAsync();
    }

    public async Task<Teacher> Update(Teacher teacher)
    {
        var curTeacher = await _schoolDbContext.Teachers.FindAsync(teacher.Id);
        if (curTeacher != null) throw new NullReferenceException("Teacher not found");
        
        curTeacher.FirstName = teacher.FirstName;
        curTeacher.MiddleName = teacher.MiddleName;
        curTeacher.LastName = teacher.LastName;
        curTeacher.BirthDate = teacher.BirthDate;
        curTeacher.Phone = teacher.Phone;
        curTeacher.Sex = teacher.Sex;
        
        await _schoolDbContext.SaveChangesAsync();
        return curTeacher;
    }

    public async Task Add(Teacher teacher)
    {
        await _schoolDbContext.Teachers.AddAsync(teacher);
        await _schoolDbContext.SaveChangesAsync();
    }
}
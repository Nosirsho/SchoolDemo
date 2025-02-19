using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class TeacherRepository : ITeacherStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public TeacherRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }
    
    public async Task<Teacher> GetById(Guid id)
    {
        var teacher = await _schoolDbContext.Teachers.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        if (teacher == null) throw new NullReferenceException($"Teacher not found with id {id}");
        return _mapper.Map<Teacher>(teacher);
    }

    public async Task<IReadOnlyList<Teacher>> GetAll()
    {
        var result = await _schoolDbContext.Teachers.Where(t=>!t.IsDeleted).ToListAsync(); 
        return _mapper.Map<IReadOnlyList<Teacher>>(result);
    }

    public async Task<Teacher> Update(Teacher teacher)
    {
        var curTeacher = await GetById(teacher.Id);
        
        curTeacher.FirstName = teacher.FirstName;
        curTeacher.MiddleName = teacher.MiddleName;
        curTeacher.LastName = teacher.LastName;
        curTeacher.BirthDate = teacher.BirthDate.ToUniversalTime();
        curTeacher.Phone = teacher.Phone;
        curTeacher.Sex = teacher.Sex;
        
        await _schoolDbContext.SaveChangesAsync();
        return curTeacher;
    }

    public async Task Add(Teacher teacher)
    {
        var teacerEntity = _mapper.Map<TeacherEntity>(teacher);
        teacerEntity.BirthDate = teacerEntity.BirthDate.ToUniversalTime();
        await _schoolDbContext.Teachers.AddAsync(teacerEntity);
        await _schoolDbContext.SaveChangesAsync();
    }

    public async Task<Guid> Delete(Guid id)
    {
        var teacher = await GetById(id);
        teacher.IsDeleted = true;
        await _schoolDbContext.SaveChangesAsync();
        return id;
    }

    public async Task<IReadOnlyList<Teacher>> Search(string text)
    {
        var result = await _schoolDbContext.Teachers
            .Where(x=> (x.LastName + " " + x.FirstName + " " + x.MiddleName).ToLower()
                .Contains(text.ToLower()) && !x.IsDeleted).ToListAsync();
        return _mapper.Map<IReadOnlyList<Teacher>>(result);
    }
}
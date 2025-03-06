using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class ParentRepository : IParentStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public ParentRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }
    public async Task<Parent?> GetById(Guid id)
    {
        var parentEntity = await _schoolDbContext.Parents.FindAsync(id);
        return _mapper.Map<Parent>(parentEntity);
    }

    public async Task<IReadOnlyList<Parent>> GetAll()
    {
        var parentEntities = await _schoolDbContext.Parents.ToListAsync();
        return _mapper.Map<IReadOnlyList<Parent>>(parentEntities); await _schoolDbContext.Parents.ToListAsync();
    }

    public async Task<Parent> Update(Parent parent)
    {
        var curParent = await _schoolDbContext.Parents.FindAsync(parent.Id);
        if (curParent==null) throw new NullReferenceException("Parent not found");
        curParent.FirstName = parent.FirstName;
        curParent.LastName = parent.LastName;
        curParent.Phone = parent.Phone;
        curParent.MiddleName = parent.MiddleName;
        curParent.Sex = parent.Sex;
        await _schoolDbContext.SaveChangesAsync();
        var result = _mapper.Map<Parent>(curParent); 
        return result;
    }

    public async Task<Parent> Add(Parent parent)
    {
        var parentEntity = _mapper.Map<ParentEntity>(parent);
        await _schoolDbContext.Parents.AddAsync(parentEntity);
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Parent>(parentEntity);
    }

    public async Task<Parent> AddWithStudent(Parent parent, Guid studentId)
    {
        var parentEntity = _mapper.Map<ParentEntity>(parent);
        await _schoolDbContext.Parents.AddAsync(parentEntity);
        await AddStudentToparent(parentEntity.Id, studentId);
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Parent>(parentEntity);
    }

    private async Task<ParentEntity> AddStudentToparent(Guid parentId, Guid studentId)
    {
        var parent = await _schoolDbContext.Parents.FindAsync(parentId);
        var student = await _schoolDbContext.Students.FindAsync(studentId);
        if (parent==null) throw new NullReferenceException("Parent not found");
        if (student==null) throw new NullReferenceException("Student not found");
        parent.Students?.Add(student);
        return parent;
    }
}
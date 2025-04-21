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
        var parentEntity = await _schoolDbContext.Parents
            .Include(p=>p.Students)
            .Where(p=> p.Id == id).FirstOrDefaultAsync();
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
        await AddStudentToParent(parentEntity.Id, studentId);
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Parent>(parentEntity);
    }

    public async Task BindParentStudents(Guid parentId, List<Guid> students)
    {
        await using var transaction = await _schoolDbContext.Database.BeginTransactionAsync();
        try
        {
            var parent = await _schoolDbContext.Parents.Include(p=>p.Students).Where(p=>p.Id == parentId).FirstOrDefaultAsync();
            if (parent==null) throw new NullReferenceException("Parent not found");
                
            foreach (var item in students)
            {
                var student = await _schoolDbContext.Students.FindAsync(item);
                if (student == null) throw new NullReferenceException($"Student not found by id {item}");
                if (parent.Students.Contains(student)) continue;
                parent.Students?.Add(student);
            }
            await _schoolDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        } catch (Exception e) {
            await transaction.RollbackAsync();
            throw new Exception($"Failed to save schedules: {e.Message}");
        }
    }

    private async Task AddStudentToParent(Guid parentId, Guid studentId)
    {
        var parent = await _schoolDbContext.Parents.FindAsync(parentId);
        var student = await _schoolDbContext.Students.FindAsync(studentId);
        if (parent==null) throw new NullReferenceException("Parent not found");
        if (student==null) throw new NullReferenceException("Student not found");
        parent.Students?.Add(student);
    }
}
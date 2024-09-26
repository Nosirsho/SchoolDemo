using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class ParentRepository : IParentStore
{
    private readonly SchoolDbContext _schoolDbContext;

    public ParentRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public async Task<Parent?> GetById(Guid id)
    {
        return await _schoolDbContext.Parents.FindAsync(id);
    }

    public async Task<IReadOnlyList<Parent>> GetAll()
    {
        return await _schoolDbContext.Parents.ToListAsync();
    }

    public async Task<Parent> Update(Parent parent)
    {
        var curParent = await _schoolDbContext.Parents.FindAsync(parent.Id);
        if (curParent==null) throw new NullReferenceException("Parent not found");
        curParent.FirstName = parent.FirstName;
        curParent.LastName = parent.LastName;
        curParent.Phone = parent.Phone;
        curParent.StudentId = parent.StudentId;
        curParent.MiddleName = parent.MiddleName;
        curParent.Sex = parent.Sex;
        await _schoolDbContext.SaveChangesAsync();
        return curParent;
    }

    public async Task Add(Parent parent)
    {
        await _schoolDbContext.Parents.AddAsync(parent);
        await _schoolDbContext.SaveChangesAsync();
    }
}
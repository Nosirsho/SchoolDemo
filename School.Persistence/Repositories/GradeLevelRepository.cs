using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class GradeLevelRepository : IGradeLevelStore
{
    private readonly SchoolDbContext _schoolDbContext;

    public GradeLevelRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public async Task<GradeLevel?> GetById(Guid id)
    {
        return await _schoolDbContext.GradeLevels.FindAsync(id);
    }

    public async Task<IReadOnlyList<GradeLevel>> GetAll()
    {
        return await _schoolDbContext.GradeLevels.ToListAsync();
    }

    public async Task<GradeLevel> Update(GradeLevel gradeLevel)
    {
        var curGradeLevel = await _schoolDbContext.GradeLevels.FindAsync(gradeLevel.Id);
        if (curGradeLevel==null) throw new NullReferenceException("GradeLevel not found");
        
        curGradeLevel.Id = gradeLevel.Id;
        curGradeLevel.Name = gradeLevel.Name;
        curGradeLevel.EntryYear = gradeLevel.EntryYear;
        await _schoolDbContext.SaveChangesAsync();
        return curGradeLevel;
    }

    public async Task Add(GradeLevel gradeLevel)
    {
        await _schoolDbContext.GradeLevels.AddAsync(gradeLevel);
        await _schoolDbContext.SaveChangesAsync();
    }
}
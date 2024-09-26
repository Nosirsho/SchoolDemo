using School.Core.Model;

namespace School.Core.Stores;

public interface IGradeLevelStore
{
    public Task<GradeLevel?> GetById(Guid id);
    public Task<IReadOnlyList<GradeLevel>> GetAll();
    public Task<GradeLevel> Update(GradeLevel gradeLevel);
    Task Add(GradeLevel gradeLevel);
}
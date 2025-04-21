using School.Core.Model;

namespace School.Core.Stores;

public interface IGradeLevelStore
{
    Task<GradeLevel?> GetById(Guid id);
    Task<IReadOnlyList<GradeLevel>> GetAll();
    Task<GradeLevel> Update(GradeLevel gradeLevel);
    Task<GradeLevel> Add(GradeLevel gradeLevel);
    Task<Guid> Delete(Guid id);
}
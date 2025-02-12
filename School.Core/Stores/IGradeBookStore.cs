using School.Core.Model;

namespace School.Core.Stores;

public interface IGradeBookStore
{
    public Task<GradeBook?> GetById(Guid id);
    public Task<IReadOnlyList<GradeBook>> GetAll();
    Task Add(GradeBook gradeLevel);
    public Task<GradeBook> Update(Guid id,
        DateTime date,
        Lesson lesson,
        Teacher teacher,
        Student student,
        int grade,
        string topic);
}
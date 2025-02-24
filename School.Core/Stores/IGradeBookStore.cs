using School.Core.Model;

namespace School.Core.Stores;

public interface IGradeBookStore
{
    public Task<GradeBook?> GetById(Guid id);
    public Task<ICollection<GradeBook>> GetAll();
    public Task<ICollection<GradeBook>> GetInterval(DateTime startDate, DateTime endDate);
    public Task<ICollection<GradeBook>> GetByLessonInterval(DateTime startDate, DateTime endDate, Guid lessonId);
    Task Add(GradeBook gradeBook);
    public Task<GradeBook> Update(Guid id,
        DateTime date,
        Lesson lesson,
        Teacher teacher,
        Student student,
        int grade,
        string topic);
}
using School.Core.Model;

namespace School.Core.Stores;

public interface IGradeBookStore
{
    public Task<GradeBook?> GetById(Guid id);
    public Task<GradeBook?> GetByCriteria(Guid studentId, Guid lessonId, DateTime date);
    public Task<ICollection<GradeBook>> GetAll();
    public Task<ICollection<GradeBook>> GetInterval(DateTime startDate, DateTime endDate);
    public Task<ICollection<GradeBook>> GetByLessonInterval(DateTime startDate, DateTime endDate, Guid lessonId, Guid? gradeLevelId);
    Task Add(GradeBook gradeBook);
    Task<IEnumerable<GradeBook>> GetStudentLessonsGrade(Guid? studentId, DateTime date);

    public Task<GradeBook> Update(Guid id,
        DateTime date,
        Lesson lesson,
        Teacher teacher,
        Student student,
        int grade,
        string topic);
    Task<Guid> Delete(Guid id);
}
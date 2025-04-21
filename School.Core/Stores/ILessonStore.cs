using School.Core.Model;

namespace School.Core.Stores;

public interface ILessonStore
{
    Task<Lesson?> GetById(Guid id);
    Task<IReadOnlyList<Lesson>> GetAll();
    Task<Lesson> Update(Lesson lesson);
    Task<Lesson> Add(Lesson lesson);
    Task<Guid> Delete(Guid id);
}
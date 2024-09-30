using School.Core.Model;

namespace School.Core.Stores;

public interface ILessonStore
{
    public Task<Lesson?> GetById(Guid id);
    public Task<IReadOnlyList<Lesson>> GetAll();
    public Task<Lesson> Update(Lesson lesson);
    Task Add(Lesson lesson);
}
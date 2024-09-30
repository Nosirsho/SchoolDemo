using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class LessonService
{
    private readonly ILessonStore _lessonStore;

    public LessonService(ILessonStore lessonStore)
    {
        _lessonStore = lessonStore;
    }
    
    public async Task<Lesson?> GetById(Guid lessonId)
    {
        return await _lessonStore.GetById(lessonId);
    }

    public async Task<IReadOnlyList<Lesson>> GetAll()
    {
        return await _lessonStore.GetAll();
    }

    public async Task<Lesson> Update(Lesson lesson)
    {
        return await _lessonStore.Update(lesson);
    }

    public async Task Create(Lesson lesson)
    {
        await _lessonStore.Add(lesson);
    }
}
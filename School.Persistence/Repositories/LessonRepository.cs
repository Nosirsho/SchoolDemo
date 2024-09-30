using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class LessonRepository : ILessonStore
{
    private readonly SchoolDbContext _schoolDbContext;

    public LessonRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public async Task<Lesson?> GetById(Guid id)
    {
        return await _schoolDbContext.Lessons.FindAsync(id);
    }

    public async Task<IReadOnlyList<Lesson>> GetAll()
    {
        return await _schoolDbContext.Lessons.ToListAsync();
    }

    public async Task<Lesson> Update(Lesson lesson)
    {
        var curLesson = await _schoolDbContext.Lessons.FindAsync(lesson.Id);
        if (curLesson==null) throw new NullReferenceException("Lesson not found");
        
        curLesson.Id = lesson.Id;
        curLesson.Name = lesson.Name;
        await _schoolDbContext.SaveChangesAsync();
        return curLesson;
    }

    public async Task Add(Lesson lesson)
    {
        await _schoolDbContext.Lessons.AddAsync(lesson);
        await _schoolDbContext.SaveChangesAsync();
    }
}
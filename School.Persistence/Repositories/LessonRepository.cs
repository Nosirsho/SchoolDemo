using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class LessonRepository : ILessonStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public LessonRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }
    public async Task<Lesson?> GetById(Guid id)
    {
        return _mapper.Map<Lesson>(await _schoolDbContext.Lessons.FindAsync(id));
    }

    public async Task<IReadOnlyList<Lesson>> GetAll()
    {
        return _mapper.Map<IReadOnlyList<Lesson>>(await _schoolDbContext.Lessons.ToListAsync()) ;
    }

    public async Task<Lesson> Update(Lesson lesson)
    {
        var curLesson = await _schoolDbContext.Lessons.FindAsync(lesson.Id);
        if (curLesson==null) throw new NullReferenceException("Lesson not found");
        
        curLesson.Id = lesson.Id;
        curLesson.Name = lesson.Name;
        await _schoolDbContext.SaveChangesAsync();
        return _mapper.Map<Lesson>(curLesson);
    }

    public async Task Add(Lesson lesson)
    {
        var lessonEntity = _mapper.Map<LessonEntity>(lesson);
        await _schoolDbContext.Lessons.AddAsync(lessonEntity);
        await _schoolDbContext.SaveChangesAsync();
    }
}
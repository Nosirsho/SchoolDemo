using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class GradeBookRepository : IGradeBookStore
{
    private readonly SchoolDbContext _context;
    private readonly IMapper _mapper;

    public GradeBookRepository(SchoolDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public Task<GradeBook?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<GradeBook>> GetAll()
    {
        var reslt  = await _context.GradeBooks
            .Include(s=>s.Student)
            .Include(s=>s.Teacher)
            .Include(s=>s.Lesson)
            .ToListAsync();
        return _mapper.Map<ICollection<GradeBook>>(await _context.GradeBooks.ToListAsync());
    }

    public Task Add(GradeBook gradeLevel)
    {
        throw new NotImplementedException();
    }

    public Task<GradeBook> Update(Guid id, DateTime date, Lesson lesson, Teacher teacher, Student student, int grade, string topic)
    {
        throw new NotImplementedException();
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

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
        var studentsWithGrades = await _context.Students
            .GroupJoin(
                _context.GradeBooks,
                student => student.Id,
                gradeBook => gradeBook.Student.Id,
                (student, gradeBooks) => new
                {
                    Student = student,
                    GradeBooks = gradeBooks
                })
            .ToListAsync();

        var result = studentsWithGrades
            .SelectMany(sg => sg.GradeBooks.DefaultIfEmpty(), (sg, gb) => new
            {
                Student = sg.Student,
                GradeBook = gb
            })
            .Select(x => x.GradeBook != null ? _mapper.Map<GradeBook>(x.GradeBook) : _mapper.Map<GradeBook>(new GradeBookEntity()
            {
                Student = x.Student,
                Date = DateTime.MinValue
            }))
            .ToList();
        return result;
    }

    public async Task<ICollection<GradeBook>> GetInterval(DateTime startDate, DateTime endDate)
    {
        var studentsWithGrades = await _context.Students
            .GroupJoin(
                _context.GradeBooks.Where(gb => gb.Date >= startDate.ToUniversalTime() && gb.Date <= endDate.ToUniversalTime()),
                student => student.Id,
                gradeBook => gradeBook.Student.Id,
                (student, gradeBooks) => new
                {
                    Student = student,
                    GradeBooks = gradeBooks
                })
            .ToListAsync();

        var result = studentsWithGrades
            .SelectMany(sg => sg.GradeBooks.DefaultIfEmpty(), (sg, gb) => new
            {
                Student = sg.Student,
                GradeBook = gb
            })
            .Select(x => x.GradeBook != null ? _mapper.Map<GradeBook>(x.GradeBook) : _mapper.Map<GradeBook>(new GradeBookEntity()
            {
                Student = x.Student,
                Date = DateTime.MinValue
            }))
            .ToList();

        return result;
    }

    public async Task<ICollection<GradeBook>> GetByLessonInterval(DateTime startDate, DateTime endDate, Guid lessonId)
    {
        var studentsWithGrades = await _context.Students
            .GroupJoin(
                _context.GradeBooks.Where(gb => gb.Date >= startDate.ToUniversalTime() && gb.Date <= endDate.ToUniversalTime() && gb.Lesson.Id == lessonId),
                student => student.Id,
                gradeBook => gradeBook.Student.Id,
                (student, gradeBooks) => new
                {
                    Student = student,
                    GradeBooks = gradeBooks
                })
            .ToListAsync();

        var result = studentsWithGrades
            .SelectMany(sg => sg.GradeBooks.DefaultIfEmpty(), (sg, gb) => new
            {
                Student = sg.Student,
                GradeBook = gb
            })
            .Select(x => x.GradeBook != null ? _mapper.Map<GradeBook>(x.GradeBook) : _mapper.Map<GradeBook>(new GradeBookEntity()
            {
                Student = x.Student,
                Date = DateTime.MinValue
            }))
            .ToList();

        return result;
    }

    public async Task Add(GradeBook gradeBook)
    {
        var gradeBookEntity = _mapper.Map<GradeBookEntity>(gradeBook);
        await _context.GradeBooks.AddAsync(gradeBookEntity);
        await _context.SaveChangesAsync();
    }

    public Task<GradeBook> Update(Guid id, DateTime date, Lesson lesson, Teacher teacher, Student student, int grade, string topic)
    {
        throw new NotImplementedException();
    }
}
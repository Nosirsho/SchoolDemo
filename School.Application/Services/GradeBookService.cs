using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class GradeBookService
{
    private readonly IGradeBookStore _gradeBookStore;

    public GradeBookService(IGradeBookStore gradeBookStore)
    {
        _gradeBookStore = gradeBookStore;
    }

    public async Task<IEnumerable<StudentGradeBook>> GetStudentGrades()
    {
        var grades = await _gradeBookStore.GetAll();
        var result = grades
            .GroupBy(s => s.Student.Id) 
            .Select(d => new StudentGradeBook
            {
                StudentId = d.Key,  // Key is now the Student.Id
                StudentFullName = d.First().Student.LastName + " " + d.First().Student.FirstName + " " + d.First().Student.MiddleName, // Get name from the first entry
                Grades = d.Select(e => new GradeBookDay
                {
                    Id = e.Id,
                    Date = e.Date.ToString("yyyy-MM-dd"),
                    Grade = e.Grade
                }).ToList()
            })
            .ToList();                                                                                                      
        return result;
    }
    
    public async Task<IEnumerable<StudentGradeBook>> GetIntervalStudentGrades(DateTime startDate, DateTime endDate)
    {
        var grades = await _gradeBookStore.GetInterval(startDate, endDate);
        var result = grades
            .GroupBy(s => s.Student.Id) 
            .Select(d => new StudentGradeBook
            {
                StudentId = d.Key,  // Key is now the Student.Id
                StudentFullName = d.First().Student.LastName + " " + d.First().Student.FirstName + " " + d.First().Student.MiddleName, // Get name from the first entry
                Grades = d.Select(e => new GradeBookDay
                {
                    Id = e.Id,
                    Date = e.Date.ToString("yyyy-MM-dd"),
                    Grade = e.Grade
                }).ToList()
            })
            .ToList();                                                                                                      
        return result;
    }
    
    public async Task<IEnumerable<StudentGradeBook>> GetByLessonIntervalStudentGrades(DateTime startDate, DateTime endDate, Guid lessonId)
    {
        var grades = await _gradeBookStore.GetByLessonInterval(startDate, endDate, lessonId);
        var result = grades
            .GroupBy(s => s.Student.Id) 
            .Select(d => new StudentGradeBook
            {
                StudentId = d.Key,  // Key is now the Student.Id
                StudentFullName = d.First().Student.LastName + " " + d.First().Student.FirstName + " " + d.First().Student.MiddleName, // Get name from the first entry
                Grades = d.Select(e => new GradeBookDay
                {
                    Id = e.Id,
                    Date = e.Date.ToString("yyyy-MM-dd"),
                    Grade = e.Grade
                }).ToList()
            })
            .ToList();                                                                                                      
        return result;
    }
    public async Task Create(GradeBook gradeBook)
    {
        await _gradeBookStore.Add(gradeBook);
    }
}
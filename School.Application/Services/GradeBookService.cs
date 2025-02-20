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
            .GroupBy(s => s.Student.Id) // Group by Student.Id for uniqueness
            .Select(d => new StudentGradeBook
            {
                StudentId = d.Key,  // Key is now the Student.Id
                StudentFullName = d.First().Student.LastName + " " + d.First().Student.FirstName + " " + d.First().Student.MiddleName, // Get name from the first entry
                Grades = d.Select(e => new GradeBookDay
                {
                    Date = e.Date.ToString("yyyy-MM-dd"),
                    Grade = e.Grade
                }).ToList()
            })
            .ToList();                                                                                                      
        return result;
    }
}
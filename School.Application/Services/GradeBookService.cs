using School.Core.Model;
using School.Core.Model.GradeBookDto;
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

    public async  Task<IEnumerable<StudentLessonGradeDto>> GetStudentLessonsGrade(Guid? studentId, DateTime date)
    {
        var gradeBooks = await _gradeBookStore.GetStudentLessonsGrade(studentId, date);
        var result = gradeBooks
            .GroupBy(gb => gb.Student.Id)
            .Select(group => new StudentLessonGradeDto
            {
                StudentId = group.Key,
                FullName = $"{group.First().Student.LastName} {group.First().Student.FirstName} {group.First().Student.MiddleName}", // Берем данные студента из первой записи группы
                LessonGrade = group
                    .Select(gb => new LessonGradeDto
                    {
                        LessonId = gb.Lesson.Id,
                        LessonName = gb.Lesson.Name,
                        Grade = gb.Grade
                    })
                    .ToList()
            })
            .ToList();
        return result;
    }

    public async Task<IEnumerable<StudentGradeBook>> GetByLessonIntervalStudentGrades(DateTime startDate, DateTime endDate, Guid lessonId, Guid? gradeBookId)
    {
        var grades = await _gradeBookStore.GetByLessonInterval(startDate, endDate, lessonId, gradeBookId);
        var result = grades
            .GroupBy(s => s.Student.Id) 
            .Select(d => new StudentGradeBook
            {
                StudentId = d.Key,
                StudentFullName = d.First().Student.LastName + " " + d.First().Student.FirstName + " " + d.First().Student.MiddleName, // Get name from the first entry
                Grades = d.Select(e => new GradeBookDay
                {
                    Id = e.Id,
                    Date = HelperService.ConvertTimeFromUtc(e.Date),
                    Grade = e.Grade
                }).ToList()
            })
            .ToList();                                                                                                      
        return result;
    }
    public async Task Create(GradeBook gradeBook)
    {
        var grade =  await _gradeBookStore.GetByCriteria(gradeBook.StudentId, gradeBook.LessonId, gradeBook.Date);
        if (grade != null)
        {
            await _gradeBookStore.Delete(grade.Id);
        }
        await _gradeBookStore.Add(gradeBook);
    }
    public async Task<Guid> Delete(Guid studentId, Guid lessonId, DateTime date)
    {
        var gradeBook =  await _gradeBookStore.GetByCriteria(studentId, lessonId, date.ToUniversalTime());
        if (gradeBook != null)
        {
            await _gradeBookStore.Delete(gradeBook.Id);
            return gradeBook.Id;
        } else {
            throw new Exception("Grade book not found for Delete");
        }
    }
}
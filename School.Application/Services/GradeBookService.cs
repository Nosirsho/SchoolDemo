using School.Core.Stores;

namespace School.Application.Services;

public class GradeBookService
{
    private readonly IGradeBookStore _gradeBookStore;

    public GradeBookService(IGradeBookStore gradeBookStore)
    {
        _gradeBookStore = gradeBookStore;
    }
}
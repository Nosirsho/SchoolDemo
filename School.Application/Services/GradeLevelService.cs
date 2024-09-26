using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class GradeLevelService
{
    private readonly IGradeLevelStore _gradeLevelStore;

    public GradeLevelService(IGradeLevelStore gradeLevelStore)
    {
        _gradeLevelStore = gradeLevelStore;
    }
    
    public async Task<GradeLevel?> GetById(Guid gradeLevelId)
    {
        return await _gradeLevelStore.GetById(gradeLevelId);
    }

    public async Task<IReadOnlyList<GradeLevel>> GetAll()
    {
        return await _gradeLevelStore.GetAll();
    }

    public async Task<GradeLevel> Update(GradeLevel gradeLevel)
    {
        return await _gradeLevelStore.Update(gradeLevel);
    }

    public async Task Create(GradeLevel gradeLevel)
    {
        await _gradeLevelStore.Add(gradeLevel);
    }
    
}
using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class TeacherService
{
    private readonly ITeacherStore _teacherStore;

    public TeacherService(ITeacherStore teacherStore)
    {
        _teacherStore = teacherStore;
    }
    
    public async Task<Teacher?> GetById(Guid teacherId)
    {
        return await _teacherStore.GetById(teacherId);
    }

    public async Task<IReadOnlyList<Teacher>> GetAll()
    {
        return await _teacherStore.GetAll();
    }

    public async Task<Teacher> Update(Teacher teacher)
    {
        return await _teacherStore.Update(teacher);
    }

    public async Task Create(Teacher teacher)
    {
        await _teacherStore.Add(teacher);
    }

    public async Task<Guid> Delete(Guid id)
    {
        return await _teacherStore.Delete(id); 
    }

    public async Task<IReadOnlyList<Teacher>> Search(string text)
    {
        var result = string.IsNullOrWhiteSpace(text) 
            ? await _teacherStore.GetAll() 
            : await _teacherStore.Search(text);
        return result;
    }

}
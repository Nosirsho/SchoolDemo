using School.Core.Model;

namespace School.Core.Stores;

public interface ITeacherStore
{
    public Task<Teacher> GetById(Guid id);
    public Task<IReadOnlyList<Teacher>> GetAll();
    public Task<Teacher> Update(Teacher teacher);
    Task Add(Teacher teacher);
    Task<Guid> Delete(Guid id);
    Task<IReadOnlyList<Teacher>> Search(string text);
}
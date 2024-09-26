using School.Core.Model;

namespace School.Core.Stores;

public interface IParentStore
{
    public Task<Parent?> GetById(Guid id);
    public Task<IReadOnlyList<Parent>> GetAll();
    public Task<Parent> Update(Parent parent);
    Task Add(Parent parent);
}
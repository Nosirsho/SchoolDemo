using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class ParentService
{
    private readonly IParentStore _parentStore;

    public ParentService(IParentStore  parentStore)
    {
        _parentStore = parentStore;
    }

    public async Task<Parent?> GetById(Guid parentId)
    {
        return await _parentStore.GetById(parentId);
    }

    public async Task<IReadOnlyList<Parent>> GetAll()
    {
        return await _parentStore.GetAll();
    }

    public async Task<Parent> Update(Parent parent)
    {
        return await _parentStore.Update(parent);
    }

    public async Task Create(Parent parent)
    {
        await _parentStore.Add(parent);
    }
}
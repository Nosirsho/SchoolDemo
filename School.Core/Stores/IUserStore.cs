using School.Core.Model;

namespace School.Core.Stores;

public interface IUserStore
{
    public Task Add(User user);
    public Task<User> GetUserByEmail(string email);
}
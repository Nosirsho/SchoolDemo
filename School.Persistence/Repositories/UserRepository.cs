using Microsoft.EntityFrameworkCore;
using School.Core.Model;
using School.Core.Stores;

namespace School.Persistence.Repositories;

public class UserRepository : IUserStore
{   
    private readonly SchoolDbContext _schoolDbContext;

    public UserRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public async Task Add(User user)
    {
        await _schoolDbContext.Users.AddAsync(user);
        await _schoolDbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _schoolDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException();
        return user;
    }
}
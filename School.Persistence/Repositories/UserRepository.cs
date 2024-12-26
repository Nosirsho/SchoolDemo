using Microsoft.EntityFrameworkCore;
using School.Core.Enums;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class UserRepository : IUserStore, IPermissionStore
{   
    private readonly SchoolDbContext _schoolDbContext;

    public UserRepository(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    public async Task Add(User user)
    {
        var roleEntity = await _schoolDbContext.Roles
            .SingleOrDefaultAsync(r=>r.Id == (int)Role.User)
            ?? throw new InvalidOperationException();
        var userEntity = new UserEntity
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Roles = [roleEntity]
        };
        await _schoolDbContext.Users.AddAsync(userEntity);
        await _schoolDbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var userEntity = await _schoolDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email) ?? throw new KeyNotFoundException();
        var user = User.Create(
            userEntity.Id,
            userEntity.UserName,
            userEntity.PasswordHash,
            userEntity.Email
            );
        return user;
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _schoolDbContext.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r=>r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();
        return roles
            .SelectMany(r => r)
            .SelectMany(r =>r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }
}
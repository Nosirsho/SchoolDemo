using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using School.Application.Interfaces.Auth;
using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class UserService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserStore _userStore;
    private readonly IPermissionStore _permissionStore;

    public UserService(
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher, 
        IUserStore userStore,
        IPermissionStore permissionStore
        )
    {
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        _userStore = userStore;
        _permissionStore = permissionStore;
    }
    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = _passwordHasher.GenerateHash(password);
        
        var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email );
        await _userStore.Add(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userStore.GetUserByEmail(email);
        var result = _passwordHasher.VerifyHash(password, user.PasswordHash);
        if (result == false)
        {
            throw new ApplicationException("Invalid password");
        }

        var permissions = await _permissionStore.GetUserPermissions(user.Id);

        var token = _jwtProvider.GenerateJWTToken(user, permissions);
        return token;
    }
}
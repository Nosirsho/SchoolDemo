using School.API.Contracts.User;
using School.Application.Services;

namespace School.API.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);
        return app;
    }

    private static async Task<IResult> Register(CreateUserRequest request, UserService userService)
    {
        await userService.Register(request.Email, request.Email, request.Password);
        return Results.Ok();
    }

    private static async Task<IResult> Login(
        LoginUserRequest request, 
        UserService userService,
        HttpContext context)
    {
        var token = await userService.Login(request.Email, request.Password);
        context.Response.Cookies.Append("test_token", token);
        return Results.Ok(token);
    }
}
using School.API.Endpoints;

namespace School.API.Extensions;

public static class ApiExtensions
{
    public static void AddMappedExtensions(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoint();
    }
}
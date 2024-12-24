using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.Parent;
using School.Application.Services;

namespace School.API.Endpoints;

public static class ParentsEndpoints
{
    public static IEndpointRouteBuilder MapParentsEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("parent");
        endpoints.MapPost(string.Empty, CreateParent);
        
        return endpoints;
    }

    private static async Task CreateParent(
        [FromBody] CreateParentRequest request,
        ParentService parentService)
    {
        
    }
}
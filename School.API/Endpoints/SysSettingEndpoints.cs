using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.SysSetting;
using School.Application.Services;
using School.Core.Model;

namespace School.API.Endpoints;

public static class SysSettingEndpoints
{
    public static IEndpointRouteBuilder MapSysSettingEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("syssetting");
        endpoints.MapGet("/{code}", GetSysSettingByCode);
        endpoints.MapPost(string.Empty, SetSysSetting);
        return endpoints;
    }

    private static async Task<IResult> GetSysSettingByCode(
        [FromRoute] string code,
        SysSettingService service
        )
    {
        var sysSetingValue = await service.GetSysSettingValueByCode(code);
        return Results.Ok(new ApiResponse<string>(sysSetingValue));
    }

    private static async Task<IResult> SetSysSetting(
        [FromBody] CreateSysSettingRequest request,
        SysSettingService service)
    {
        var sysSettingId = await service.CreateSysSetting(request.Code, request.TypeId, request.Value);
        return Results.Ok(new ApiResponse<Guid>(sysSettingId));
    }
}
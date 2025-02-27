using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.SysSetting;
using School.Application.Services;
using School.Core.Constants;
using School.Core.Enums;
using School.Core.Model;

namespace School.API.Endpoints;

public static class SysSettingEndpoints
{
    public static IEndpointRouteBuilder MapSysSettingEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("syssetting");
        endpoints.MapGet("/{code}", GetSysSettingByCode);
        endpoints.MapGet("/type", GetSysSettingTypes);
        endpoints.MapGet(string.Empty, GetSysSettings);
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
        try
        {
            var sysSettingId = await service.CreateSysSetting(request.Code, request.TypeId, request.Value);
            return Results.Ok(new ApiResponse<Guid>(sysSettingId));
        }
        catch (Exception e)
        {
            return Results.Ok(new ApiResponse<object>(0,e.Message));
        }
    }

    private static IResult GetSysSettingTypes()
    {
        var types = Enum
            .GetValues<SysSettingType>()
            .Select( p => new GetSysSettingResponse(BaseConstant.GetByName(p.ToString()), (int)p,p.ToString()));
        var result = new ApiResponse<IEnumerable<GetSysSettingResponse>>(types);
        return Results.Ok(result);
    }
    private static async Task<IResult> GetSysSettings(
        SysSettingService service
        )
    {
        try
        {
            var sysSettings = await service.GetSysSettingList();
            var sysSettingList = sysSettings.Select(sysSetting => new GetSysSettingListResponse(sysSetting.Id, sysSetting.Name, sysSetting.Code, sysSetting.Type.ToString(), sysSetting.IntegerValue, sysSetting.DateTimeValue.ToString("yyyy-MM-dd"), sysSetting.BooleanValue, sysSetting.StringValue, sysSetting.GuidValue));
            var result = new ApiResponse<IEnumerable<GetSysSettingListResponse>>(sysSettingList);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            var result = new ApiResponse<object>(0, e.Message);
            return Results.Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using School.Application.Services;

namespace School.API.Endpoints;

public static class SmsSenderEndpoints
{
    public static IEndpointRouteBuilder MapSmsSenderEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("smssender");
        endpoints.MapPost(string.Empty, SendSms);
        return endpoints;
    }
    
    private static async Task<IResult> SendSms(HttpContext context, 
        SmsService servise,
        [FromBody] string meesage
        )
    {
        var result = await servise.SendSms("992927400719", meesage);
        return Results.Ok(result);
    }
}
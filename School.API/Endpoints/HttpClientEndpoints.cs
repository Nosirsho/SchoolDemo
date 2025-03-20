using Microsoft.AspNetCore.Mvc;
using School.API.Contracts.GradeLevel;
using School.Application.Services;
using School.Core.Model;
using School.Core.Model.SMSModel;

namespace School.API.Endpoints;

public static class HttpClientEndpoints
{
    public static IEndpointRouteBuilder MapHttpClientEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("sms");
        endpoints.MapGet("/", GetGradeLevelById);
        endpoints.MapGet("/send", SendSms);
        return endpoints;
    }

    private static async Task<IResult> GetGradeLevelById(
        IHttpClientFactory clientFactory
        )
    {
        var client = clientFactory.CreateClient("SMS");
        var result = await client.GetFromJsonAsync<ApiResponse2>("?city=2&currency=3");
        return Results.Ok( new ApiResponse<ApiResponse2>(result));
    }

    private static async Task<IResult> SendSms(
        NotificationService service
        )
    {
        // var result = await service.NotifyAsync("Hello World!");
        // return Results.Ok(new ApiResponse<BaseSmsResponse>(result));
        return Results.Ok();
    }

    public class City
    {
        public int city_id { get; set; }
        public string city_name { get; set; }
        public bool has_active_packages { get; set; }
    }

    public class ApiResponse2
    {
        public List<object> banks { get; set; } // Предполагаем, что banks - это массив объектов (или пустой массив)
        public List<City> cities { get; set; }
    }
}
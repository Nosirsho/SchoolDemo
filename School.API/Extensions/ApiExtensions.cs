using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.API.Endpoints;
using School.Infrastructure;

namespace School.API.Extensions;

public static class ApiExtensions
{
    public static void AddMappedExtensions(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoint();
    }

    public static void AddApiAuthentication(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["test_token"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim("Admin", "true");
            });
            options.AddPolicy("StudentPolicy", policy =>
            {
                policy.RequireClaim("Student", "true");
            });
        });
        
        services.AddAuthentication();
    }
    
}
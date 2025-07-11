﻿using Api.Infrastructure.Gateways.Zibal;
using Api.Infrastructure.JwtUtil;
using AspNetCoreRateLimit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddAutoMapper(typeof(MapperProfile).Assembly);
        service.AddTransient<CustomJwtValidation>();
        service.AddHttpClient<IZibalService, ZibalService>();

        service.AddCors(options =>
        {
            options.AddPolicy(name: "ShopApi",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        service.AddMemoryCache();

        //load general configuration from appsettings.json
        service.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        //load ip rules from appsettings.json
        service.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        // inject counter and rules stores
        service.AddInMemoryRateLimiting();

        service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}
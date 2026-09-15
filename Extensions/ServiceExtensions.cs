using checkBack.Services;
using checkBack.Services.Interfaces;

namespace checkBack.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        // Singleton so in-memory task state persists across requests
        services.AddSingleton<ITaskService, TaskService>();
        return services;
    }
}

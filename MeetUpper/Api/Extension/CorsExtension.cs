namespace Api.Extension;

public static class CorsExtension
{
    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(configure=>
            configure.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            }));
        return services;
    }
}
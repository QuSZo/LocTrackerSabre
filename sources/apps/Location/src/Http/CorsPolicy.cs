namespace Location.Http;

public static class CorsPolicy
{
    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(p =>
                p.WithOrigins("https://web-273348683080.europe-central2.run.app")
                .AllowAnyHeader()
                .AllowAnyMethod());
        });
    }
}
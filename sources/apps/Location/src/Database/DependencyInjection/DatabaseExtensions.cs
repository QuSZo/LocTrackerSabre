using Location.Database.Repositories;

namespace Location.Database.DependencyInjection;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseConfiguration>();
        services.AddSingleton<DbContext>();
        services.AddSingleton<DeviceLocationRepository>();
    }
}
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MOVIES.Application.Database;
using MOVIES.Application.Repositories;
using MOVIES.Application.Services;

namespace MOVIES.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();
        services.AddSingleton<IRatingRepository, RatingRepository>();
        services.AddSingleton<IMovieService, MovieSerivce>();
        services.AddSingleton<IRatingService, RatingService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();

        return services;
    }
}

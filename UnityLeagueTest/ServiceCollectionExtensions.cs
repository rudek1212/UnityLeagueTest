using UnityLeagueTest.Services;

namespace UnityLeagueTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UnityLeagueConnectionSettings>(configuration.GetSection("UnityLeagueConnectionSettings"));

        services.AddTransient<UnityLeagueAuthHandler>();

        services.AddHttpClient<IUnityLeagueAuthService, UnityLeagueAuthService>(client =>
        {
            var baseUrl = configuration["UnityLeagueConnectionSettings:BaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "UnityLeague BaseUrl is not configured.");

            client.BaseAddress = new Uri(baseUrl);
        });

        services.AddHttpClient<IUnityLeagueClient, UnityLeagueClient>(client =>
            {
                var baseUrl = configuration["UnityLeagueConnectionSettings:BaseUrl"];
                if (string.IsNullOrWhiteSpace(baseUrl))
                    throw new ArgumentNullException(nameof(baseUrl), "UnityLeague BaseUrl is not configured.");

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<UnityLeagueAuthHandler>();

        return services;
    }
}
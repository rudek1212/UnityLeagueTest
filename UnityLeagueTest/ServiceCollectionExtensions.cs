namespace UnityLeagueTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {

        services.AddHttpClient<IUnityLeagueClient, UnityLeagueClient>()
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false
            });
            //.AddHttpMessageHandler(() =>
            //    new PreserveAuthorizationRedirectHandler("rudnickibartosz95@gmail.com", "1qaz!QAZ"));


        return services;
    }
}
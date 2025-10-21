using System.Net.Http.Headers;
using UnityLeagueTest.Services;

namespace UnityLeagueTest;

public class UnityLeagueAuthHandler : DelegatingHandler
{
    private readonly IUnityLeagueAuthService _authService;

    public UnityLeagueAuthHandler(IUnityLeagueAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _authService.GetAccessTokenAsync();

        if (!request.Headers.Contains("Authorization"))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
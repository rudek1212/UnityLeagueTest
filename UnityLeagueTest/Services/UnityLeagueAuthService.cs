using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using UnityLeagueTest.Models;
using UnityLeagueTest.Transfer.Response;

namespace UnityLeagueTest.Services;

public interface IUnityLeagueAuthService
{
    Task<string> GetAccessTokenAsync();
}

public class UnityLeagueAuthService : IUnityLeagueAuthService
{
    private readonly HttpClient _httpClient;
    private readonly UnityLeagueConnectionSettings? _settings;
    private UnityLeagueToken? _cachedToken;

    public UnityLeagueAuthService(
        HttpClient httpClient,
        IOptions<UnityLeagueConnectionSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        if (_cachedToken is { IsExpired: false })
            return _cachedToken.AccessToken;

        var token = await GetBearerTokenAsync();


        _cachedToken = new UnityLeagueToken
        {
            AccessToken = token.Token,
            ExpiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn - 60)
        };

        return _cachedToken.AccessToken;
    }

    private async Task<TokenResponse?> GetBearerTokenAsync()
    {
        var url = $"{_settings.BaseUrl}/o/token/";

        var data = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", _settings.Username },
            { "password", _settings.Password }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new FormUrlEncodedContent(data)
        };

        request.Headers.Authorization = GetBasicAuthHeader(_settings.ClientId, _settings.ClientSecret);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TokenResponse>();
    }

    private AuthenticationHeaderValue GetBasicAuthHeader(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException(nameof(username));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        var credentials = $"{username}:{password}";
        var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

        return new AuthenticationHeaderValue("Basic", base64Credentials);
    }
}
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using UnityLeagueTest.Dto;

namespace UnityLeagueTest;

public interface IUnityLeagueClient
{
    public Task<UserDto?> GetCurrentUserAsync();
    public Task<OrganizerDto?> GetCurrentOrganizerAsync();
    public Task<TokenDto?> GetBearerTokenAsync();
    public Task<string?> CreateEventAsync();
}

public class UnityLeagueClient : IUnityLeagueClient
{
    private readonly UnityLeagueConnectionSettings _options;
    private readonly HttpClient _httpClient;

    public UnityLeagueClient(
        HttpClient httpClient,
        IOptions<UnityLeagueConnectionSettings> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync($"{_options.BaseUrl}/api/me/");

        _httpClient.DefaultRequestHeaders.Authorization = await GetBearerTokenHeaderAsync();

        await HandleResponse(response);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserDto>(responseJson);
    }

    public async Task<OrganizerDto?> GetCurrentOrganizerAsync()
    {
        var response = await _httpClient.GetAsync($"{_options.BaseUrl}/api/organizers/me/");

        _httpClient.DefaultRequestHeaders.Authorization = await GetBearerTokenHeaderAsync();

        await HandleResponse(response);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<OrganizerDto>(responseJson);
    }

    public async Task<string?> CreateEventAsync()
    {
        var eventDto = new EventDto
        {
            Name = "API Christmas Event",
            Date = "2025-12-25",
            StartTime = "13:00:00",
            EndTime = "20:00:00",
            Format = "LEGACY",
            Category = "PREMIER",
            Url = "https://test.example",
            Description = "This is going to be <b>very</b> cool!",
        };

        var url = $"{_options.BaseUrl}/api/events/";


        _httpClient.DefaultRequestHeaders.Authorization = await GetBearerTokenHeaderAsync();
        var response = await _httpClient.PostAsJsonAsync(url, eventDto);

        await HandleResponse(response);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<TokenDto?> GetBearerTokenAsync()
    {
        var url = $"{_options.BaseUrl}/o/token/";

        var data = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", _options.Username },
            { "password", _options.Password }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new FormUrlEncodedContent(data)
        };

        request.Headers.Authorization = GetBasicAuthHeader(_options.ClientId, _options.ClientSecret);

        var response = await _httpClient.SendAsync(request);
        
        await HandleResponse(response);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TokenDto>(responseJson);
    }

    private async Task<AuthenticationHeaderValue> GetBearerTokenHeaderAsync()
    {
        var token = await GetBearerTokenAsync();

        return new AuthenticationHeaderValue("Bearer", token.Token);
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


    private async Task HandleResponse(HttpResponseMessage? response)
    {
        if (response.StatusCode == HttpStatusCode.MovedPermanently)
            throw new Exception($"Response code: {response.StatusCode:D}. Location: {response.Headers.Location}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response code: {response.StatusCode:D} {response.StatusCode:G}. Reason: {response.ReasonPhrase}. {await response.Content.ReadAsStringAsync()}.");
    }
}
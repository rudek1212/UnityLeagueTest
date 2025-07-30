using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
    private string? _baseUrl = "https://playground.unityleague.ch";
    private string? _clientId = "CLIENTID";
    private string? _clientSecret = "CLIENTSECRET";

    private readonly HttpClient _httpClient;

    public UnityLeagueClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto?> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/me/");

        _httpClient.DefaultRequestHeaders.Authorization = await GetBearerTokenHeaderAsync();

        await HandleResponse(response);

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserDto>(responseJson);
    }

    public async Task<OrganizerDto?> GetCurrentOrganizerAsync()
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/organizers/me/");

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
            Address = 483,
            SubmitType = "create_event"
        };

        var url = $"{_baseUrl}/api/events/create/";

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = await GetBearerTokenHeaderAsync();
        var response = await client.PostAsJsonAsync(url, eventDto);

        await HandleResponse(response);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<TokenDto?> GetBearerTokenAsync()
    {
        var data = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" },
            { "client_id", _clientId },
            { "client_secret", _clientSecret }
        });

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/o/token/");
        request.Content = data;

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

    private async Task HandleResponse(HttpResponseMessage? response)
    {
        if (response.StatusCode == HttpStatusCode.MovedPermanently)
            throw new Exception($"Response code: {response.StatusCode:D}. Location: {response.Headers.Location}");
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Response code: {response.StatusCode:D} {response.StatusCode:G}. Reason: {response.ReasonPhrase}. {await response.Content.ReadAsStringAsync()}.");
    }
}
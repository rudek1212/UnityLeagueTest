using Newtonsoft.Json;
using System.Text;
using UnityLeagueTest.Transfer.Dto;
using UnityLeagueTest.Transfer.Mapper;
using UnityLeagueTest.Transfer.Request;
using UnityLeagueTest.Transfer.Response;

namespace UnityLeagueTest
{
    public interface IUnityLeagueClient
    {
        Task<EventDetailsDto?> GetEventByIdAsync(int eventId, CancellationToken cancellationToken = default);
        Task<EventDetailsDto?> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default);
        Task<EventDetailsDto?> UpdateEventAsync(int eventId, UpdateEventRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteEventAsync(int eventId, CancellationToken cancellationToken = default);
        Task<EventDetailsDto?> ReportResultsAsync(int eventId, ReportResultsRequest request, CancellationToken cancellationToken = default);
    }

    public class UnityLeagueClient : IUnityLeagueClient
    {
        private readonly HttpClient _httpClient;

        public UnityLeagueClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EventDetailsDto?> GetEventByIdAsync(int eventId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"/api/events/{eventId}/", cancellationToken);
            if (!response.IsSuccessStatusCode) return null;

            var responseData = await response.Content.ReadFromJsonAsync<CreateEventResponse>(cancellationToken: cancellationToken);
            return responseData.MapToDto();
        }

        public async Task<EventDetailsDto?> CreateEventAsync(CreateEventRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(
                "/api/events/",
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"),
                cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<CreateEventResponse>(cancellationToken: cancellationToken);

            return responseData.MapToDto();
        }

        public async Task<EventDetailsDto?> UpdateEventAsync(int eventId, UpdateEventRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PutAsync(
                $"/api/events/{eventId}/",
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"),
                cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<UpdateEventResponse>(cancellationToken: cancellationToken);

            return responseData.MapToDto();
        }

        public async Task<bool> DeleteEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"/api/events/{eventId}/", cancellationToken);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Raportuje wyniki do wydarzenia
        /// POST /api/events/{id}/report/
        /// </summary>
        public async Task<EventDetailsDto> ReportResultsAsync(int eventId, ReportResultsRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PatchAsync(
                $"/api/events/{eventId}/",
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"),
                cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadFromJsonAsync<ReportResultsResponse>(cancellationToken: cancellationToken);

            return responseData.MapToDto();
        }
    }
}

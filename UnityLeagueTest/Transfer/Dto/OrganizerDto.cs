using System.Text.Json.Serialization;

namespace UnityLeagueTest.Transfer.Dto;

public class OrganizerDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("events")]
    public string[]? Events { get; set; }
}
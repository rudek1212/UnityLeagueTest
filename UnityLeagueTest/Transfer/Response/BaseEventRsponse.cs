using System.Text.Json.Serialization;

namespace UnityLeagueTest.Transfer.Request;

public class BaseEventRsponse
{
    [JsonPropertyName("api_url")]
    public string ApiUrl { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public string EndTime { get; set; }

    [JsonPropertyName("format")]
    public string Format { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("organizer")]
    public string OrganizerUrl { get; set; }

    [JsonPropertyName("results")]
    public ReportMatchResult[] Results { get; set; }
}

using System.Text.Json.Serialization;

namespace UnityLeagueTest.Dto;

public class EventDto
{
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

    [JsonPropertyName("address")]
    public int Address { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("submit_type")]
    public string SubmitType { get; set; }
}
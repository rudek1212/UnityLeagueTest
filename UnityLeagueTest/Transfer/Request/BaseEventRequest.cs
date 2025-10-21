using Newtonsoft.Json;

namespace UnityLeagueTest.Transfer.Request;

public class BaseEventRequest
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("start_time")]
    public string StartTime { get; set; }

    [JsonProperty("end_time")]
    public string EndTime { get; set; }

    [JsonProperty("format")]
    public string Format { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("api_url")]
    public string Url { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}

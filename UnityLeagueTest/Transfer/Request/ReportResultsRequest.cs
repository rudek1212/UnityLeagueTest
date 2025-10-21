using Newtonsoft.Json;

namespace UnityLeagueTest.Transfer.Request;

public class ReportResultsRequest
{
    [JsonProperty("results")]
    public List<ReportMatchResult> Results { get; set; }
}
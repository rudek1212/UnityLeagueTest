using Newtonsoft.Json;

namespace UnityLeagueTest.Transfer;

public class ReportMatchResult
{
    [JsonProperty("player")]
    public string PlayerName { get; set; }

    [JsonProperty("win_count")]
    public int WinCount { get; set; }

    [JsonProperty("draw_count")]
    public int DrawCount { get; set; }

    [JsonProperty("loss_count")]
    public int LossCount { get; set; }

    [JsonProperty("single_elimination_result")]
    public SingleEliminationResult? SingleEliminationResult { get; set; }
}

public enum SingleEliminationResult
{
    Winner = 1,
    Finalist = 2,
    SemiFinalist = 4,
    QuarterFinalist = 8
}
namespace UnityLeagueTest.Transfer.Dto;

public class ReportMatchResultDto
{
    public string PlayerName { get; set; }
    public int WinCount { get; set; }
    public int DrawCount { get; set; }
    public int LossCount { get; set; }
    public SingleEliminationResult? SingleEliminationResult { get; set; }
}
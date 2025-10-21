namespace UnityLeagueTest.Transfer.Dto;

public class EventDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public string Format { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public ReportMatchResultDto[] Results { get; set; }
}
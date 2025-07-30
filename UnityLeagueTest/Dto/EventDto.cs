namespace UnityLeagueTest.Dto;

public class EventDto
{
    public string Name { get; set; } = default!;
    public string Date { get; set; } = default!; // format: yyyy-MM-dd
    public string StartTime { get; set; } = default!; // format: HH:mm:ss
    public string EndTime { get; set; } = default!; // format: HH:mm:ss
    public string Format { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Description { get; set; } = default!;
}
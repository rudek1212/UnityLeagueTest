using System.Globalization;
using UnityLeagueTest.Transfer.Dto;
using UnityLeagueTest.Transfer.Response;

namespace UnityLeagueTest.Transfer.Mapper;

public static class EventDetailsDtoMapper
{
    public static EventDetailsDto MapToDto(this CreateEventResponse source)
    {
        return new EventDetailsDto
        {
            Id = Convert.ToInt32(source.ApiUrl.Split('/').TakeLast(2).First()),
            Name = source.Name,
            StartDate = DateTime.ParseExact($"{source.Date} {source.StartTime}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            Format = source.Format,
            Category = source.Category,
            Description = source.Description,
            Results = source.Results.Select(x => x.MapToDto()).ToArray()
        };
    }

    public static EventDetailsDto MapToDto(this UpdateEventResponse source)
    {
        return new EventDetailsDto
        {
            Id = Convert.ToInt32(source.ApiUrl.Split('/').TakeLast(2).First()),
            Name = source.Name,
            StartDate = DateTime.ParseExact($"{source.Date} {source.StartTime}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            Format = source.Format,
            Category = source.Category,
            Description = source.Description,
            Results = source.Results.Select(x => x.MapToDto()).ToArray()
        };
    }
    public static EventDetailsDto MapToDto(this ReportResultsResponse source)
    {
        return new EventDetailsDto
        {
            Id = Convert.ToInt32(source.ApiUrl.Split('/').TakeLast(2).First()),
            Name = source.Name,
            StartDate = DateTime.ParseExact($"{source.Date} {source.StartTime}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
            Format = source.Format,
            Category = source.Category,
            Description = source.Description,
            Results = source.Results.Select(x => x.MapToDto()).ToArray()
        };
    }
}

public static class ReportMatchResultDtoMapper
{
    public static ReportMatchResultDto MapToDto(this ReportMatchResult source)
    {
        return new ReportMatchResultDto
        {
            PlayerName = source.PlayerName,
            WinCount = source.WinCount,
            DrawCount = source.DrawCount,
            LossCount = source.LossCount,
            SingleEliminationResult = source.SingleEliminationResult
        };
    }
}
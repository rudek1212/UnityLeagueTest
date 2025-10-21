using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UnityLeagueTest.Transfer.Dto;

public class UserDto
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
}
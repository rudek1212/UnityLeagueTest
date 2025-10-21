namespace UnityLeagueTest.Models;

public class UnityLeagueToken
{
    public string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
}
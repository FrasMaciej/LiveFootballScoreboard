namespace LiveFootballScoreboard.Models;
internal class Match
{
    public required Team HomeTeam { get; set; }
    public required Team AwayTeam { get; set; }
    public DateTime MatchStartDateTime { get; set; }
}

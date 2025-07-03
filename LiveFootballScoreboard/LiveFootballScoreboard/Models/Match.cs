namespace LiveFootballScoreboard.Models;
internal class Match
{
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public DateTime MatchStartDateTime { get; set; }
    public Guid id;
}

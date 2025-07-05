namespace LiveFootballScoreboard.Contracts;
public class MatchResult
{
    public required string HomeTeam { get; set; }
    public int HomeScore { get; set; }
    public required string AwayTeam { get; set; }
    public int AwayScore { get; set; }
}

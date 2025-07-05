namespace LiveFootballScoreboard.Contracts;
public class MatchesSummary
{
    public required IEnumerable<MatchResult> MatchResults { get; set; } 
}

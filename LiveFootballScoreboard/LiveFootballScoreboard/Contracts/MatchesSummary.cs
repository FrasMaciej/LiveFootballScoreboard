namespace LiveFootballScoreboard.Contracts;

/// <summary>
/// Represents a summary of all ongoing matches.
/// </summary>
public class MatchesSummary
{
    internal MatchesSummary(IReadOnlyList<MatchResult> matchResults)
    {
        MatchResults = matchResults;
    }

    /// <summary>
    /// Gets the list of match results.
    /// </summary>
    public IReadOnlyList<MatchResult> MatchResults { get; } 
}

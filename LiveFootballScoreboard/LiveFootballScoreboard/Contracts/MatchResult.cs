namespace LiveFootballScoreboard.Contracts;

/// <summary>
/// Represents the result of a football match.
/// </summary>
public class MatchResult
{
    internal MatchResult(string homeTeam, int homeScore, string awayTeam, int awayScore)
    {
        HomeTeam = homeTeam;
        HomeScore = homeScore;
        AwayTeam = awayTeam;
        AwayScore = awayScore;
    }

    /// <summary>
    /// Gets the name of the home team.
    /// </summary>
    public string HomeTeam { get; }

    /// <summary>
    /// Gets the score of the home team.
    /// </summary>
    public int HomeScore { get; }

    /// <summary>
    /// Gets the name of the away team.
    /// </summary>
    public string AwayTeam { get; }

    /// <summary>
    /// Gets the score of the away team.
    /// </summary>
    public int AwayScore { get; }
}

namespace LiveFootballScoreboard.Contracts;

/// <summary>
/// Defines the contract for a scoreboard service that manages live football matches.
/// </summary>
public interface IScoreboard
{
    /// <summary>
    /// Starts a new match with the given home and away teams.
    /// </summary>
    /// <param name="homeTeam">The name of the home team.</param>
    /// <param name="awayTeam">The name of the away team.</param>
    /// <returns>The unique identifier of the newly created match.</returns>
    /// <exception cref="ArgumentException">Thrown if team names are invalid or if teams are already playing.</exception>
    Guid StartMatch(string homeTeam, string awayTeam);

    /// <summary>
    /// Updates the score of an ongoing match.
    /// </summary>
    /// <param name="homeTeam">The name of the home team</param>
    /// <param name="awayTeam">The name of the away team</param>
    /// <param name="homeTeamScore">The new score for the home team.</param>
    /// <param name="awayTeamScore">The new score for the away team.</param>
    /// <exception cref="ArgumentException">Thrown if scores are negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the match is not found.</exception>
    void UpdateScore(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore);

    /// <summary>
    /// Updates the score of an ongoing match.
    /// </summary>
    /// <param name="matchGuid">The unique identifier of the match.</param>
    /// <param name="homeTeamScore">The new score for the home team.</param>
    /// <param name="awayTeamScore">The new score for the away team.</param>
    /// <exception cref="ArgumentException">Thrown if scores are negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the match is not found.</exception>
    void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore);

    /// <summary>
    /// Finishes an ongoing match and removes it from the scoreboard.
    /// </summary>
    /// <param name="matchGuid">The unique identifier of the match.</param>
    /// <exception cref="KeyNotFoundException">Thrown if the match is not found.</exception>
    void FinishMatch(Guid matchGuid);

    /// <summary>
    /// Finishes an ongoing match and removes it from the scoreboard.
    /// </summary>
    /// <param name="homeTeam">The name of the home team.</param>
    /// <param name="awayTeam">The name of the awayTeam team.</param>
    /// <exception cref="KeyNotFoundException">Thrown if the match is not found.</exception>
    void FinishMatch(string homeTeam, string awayTeam);

    /// <summary>
    /// Retrieves a summary of all ongoing matches, ordered firstly by total score and then by start time.
    /// </summary>
    /// <returns>A summary of matches in progress.</returns>
    MatchesSummary GetSummary();
}

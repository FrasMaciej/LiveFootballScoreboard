using LiveFootballScoreboard.Services;

namespace LiveFootballScoreboard.Contracts;

/// <summary>
/// Factory class for creating instances of the scoreboard service.
/// </summary>
public class ScoreboardFactory
{
    /// <summary>
    /// Creates a new instance of the scoreboard service.
    /// </summary>
    /// <returns>An instance of <see cref="IScoreboard"/>.</returns>
    public static IScoreboard Create() => new ScoreboardService();
}

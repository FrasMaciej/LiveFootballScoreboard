using LiveFootballScoreboard.Services;

namespace LiveFootballScoreboard.Contracts;
public class ScoreboardFactory
{
    public static IScoreboard Create() => new ScoreboardService();
}

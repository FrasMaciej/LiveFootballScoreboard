using LiveFootballScoreboard.Contracts;
using LiveFootballScoreboard.Models;

namespace LiveFootballScoreboard.Services;

internal class ScoreboardService : IScoreboard
{
    private Scoreboard _scoreboard;

    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        throw new NotImplementedException();
    }

    public void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore)
    {
        throw new NotImplementedException();
    }

    public void FinishMatch(Guid matchGuid)
    {
        throw new NotImplementedException();
    }

    public void GetSummary()
    {
        throw new NotImplementedException();
    }
}

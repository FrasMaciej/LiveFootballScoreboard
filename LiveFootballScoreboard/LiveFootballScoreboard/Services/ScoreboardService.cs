using System.Collections.ObjectModel;
using LiveFootballScoreboard.Models;

namespace LiveFootballScoreboard.Services;
public class ScoreboardService
{
    private Scoreboard _scoreboard;

    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        throw new NotImplementedException();
    }

    void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore)
    {
        throw new NotImplementedException();
    }

    void FinishMatch(Guid matchGuid)
    {
        throw new NotImplementedException();
    }

    void GetSummary()
    {
        throw new NotImplementedException();
    }
}

namespace LiveFootballScoreboard.Contracts;

public interface IScoreboard
{
    public Guid StartMatch(string homeTeam, string awayTeam);

    public void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore);

    public void FinishMatch(Guid matchGuid);

    public MatchesSummary GetSummary();
}

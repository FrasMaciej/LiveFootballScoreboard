namespace LiveFootballScoreboard.Contracts;

public interface IScoreboard
{
    public Guid StartMatch(string homeTeam, string awayTeam);

    void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore);

    void FinishMatch(Guid matchGuid);

    void GetSummary();
}

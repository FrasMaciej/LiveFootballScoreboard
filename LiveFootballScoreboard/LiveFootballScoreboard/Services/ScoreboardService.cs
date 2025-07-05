using LiveFootballScoreboard.Contracts;
using LiveFootballScoreboard.Models;

namespace LiveFootballScoreboard.Services;

internal class ScoreboardService : IScoreboard
{
    private readonly Dictionary<Guid, Match> _matchesById = [];

    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        if (string.IsNullOrWhiteSpace(homeTeam))
            throw new ArgumentException("Team names cannot be null or empty.", nameof(homeTeam));
        if (string.IsNullOrWhiteSpace(awayTeam))
            throw new ArgumentException("Team names cannot be null or empty.", nameof(awayTeam));

        var homeTeamNormalized = homeTeam.Trim().ToLowerInvariant();
        var awayTeamNormalized = awayTeam.Trim().ToLowerInvariant();

        if (homeTeamNormalized == awayTeamNormalized)
            throw new ArgumentException("A team cannot play against itself.");

        var teamsInPlay = _matchesById.Values
            .SelectMany(m => new[] { m.HomeTeam.Name.ToLowerInvariant(), m.AwayTeam.Name.ToLowerInvariant() })
            .ToHashSet();

        var homeTeamPlayingMatch = teamsInPlay.Contains(homeTeamNormalized);
        var awayTeamPlayingMatch = teamsInPlay.Contains(awayTeamNormalized);
        if (homeTeamPlayingMatch && awayTeamPlayingMatch)
            throw new ArgumentException("Both teams are already playing a match.");
        if (homeTeamPlayingMatch)
            throw new ArgumentException("Home team is already playing a match.", nameof(homeTeam));
        if (awayTeamPlayingMatch)
            throw new ArgumentException("Away team is already playing a match.", nameof(awayTeam));


        var newMatchId = Guid.NewGuid();
        var match = new Match
        {
            HomeTeam = new Team { Name = homeTeam },
            AwayTeam = new Team { Name = awayTeam },
            MatchStartDateTime = DateTime.UtcNow
        };

        _matchesById[newMatchId] = match;

        return newMatchId;
    }

    public void UpdateScore(Guid matchGuid, int homeTeamScore, int awayTeamScore)
    {
        if (homeTeamScore < 0 || awayTeamScore < 0)
            throw new ArgumentException("Scores must be non-negative.");

        if (!_matchesById.TryGetValue(matchGuid, out var match))
            throw new KeyNotFoundException($"No match found with ID {matchGuid}.");

        match.HomeTeam.Score = homeTeamScore;
        match.AwayTeam.Score = awayTeamScore;
    }

    public void FinishMatch(Guid matchGuid)
    {
        if (!_matchesById.Remove(matchGuid))
            throw new KeyNotFoundException($"No match found with ID {matchGuid}.");
    }

    public MatchesSummary GetSummary()
    {
        var matches = _matchesById.Values
            .OrderByDescending(m => m.HomeTeam.Score + m.AwayTeam.Score)
            .ThenByDescending(m => m.MatchStartDateTime)
            .Select(m => new MatchResult
            {
                HomeTeam = m.HomeTeam.Name,
                HomeScore = m.HomeTeam.Score,
                AwayTeam = m.AwayTeam.Name,
                AwayScore = m.AwayTeam.Score
            });

        return new MatchesSummary { MatchResults = matches };
    }
}

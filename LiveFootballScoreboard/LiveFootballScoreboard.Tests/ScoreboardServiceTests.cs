using FluentAssertions;
using LiveFootballScoreboard.Contracts;
using LiveFootballScoreboard.Services;

namespace LiveFootballScoreboard.Tests;

public class ScoreboardServiceTests
{
    private readonly IScoreboard _scoreboard = new ScoreboardService();

    [Fact]
    public void StartMatch_ShouldAddMatchToScoreboardWithZeroZeroResult()
    {
        // Act
        _scoreboard.StartMatch("Team A", "Team B");

        // Assert
        var summary = _scoreboard.GetSummary();
        summary.MatchResults.Should().ContainSingle(match =>
            match.HomeTeam == "Team A" &&
            match.AwayTeam == "Team B" &&
            match.HomeScore == 0 &&
            match.AwayScore == 0);
    }

    [Fact]
    public void UpdateScore_ShouldUpdateMatchScores()
    {
        // Arrange
        var matchId = _scoreboard.StartMatch("Team A", "Team B");

        // Act
        _scoreboard.UpdateScore(matchId, 3, 2);

        // Assert
        var summary = _scoreboard.GetSummary();
        summary.MatchResults.Should().ContainSingle(match =>
            match.HomeTeam == "Team A" &&
            match.AwayTeam == "Team B" &&
            match.HomeScore == 3 &&
            match.AwayScore == 2);
    }

    [Fact]
    public void FinishMatch_ShouldRemoveMatchFromScoreboard()
    {
        // Arrange
        var matchId = _scoreboard.StartMatch("Team A", "Team B");

        // Act
        _scoreboard.FinishMatch(matchId);

        // Assert
        var summary = _scoreboard.GetSummary();
        summary.MatchResults.Should().BeEmpty();
    }

    [Fact]
    public void GetSummary_ShouldReturnMatchesOrderedByScoreAndStartTime()
    {
        // Arrange
        var match1Id = _scoreboard.StartMatch("Team A", "Team B"); 
        var match2Id = _scoreboard.StartMatch("Team C", "Team D");
        var match3Id = _scoreboard.StartMatch("Team E", "Team F"); 

        _scoreboard.UpdateScore(match1Id, 1, 0); 
        _scoreboard.UpdateScore(match2Id, 2, 2);
        _scoreboard.UpdateScore(match3Id, 1, 1);

        // Act
        var summary = _scoreboard.GetSummary();

        // Assert
        summary.MatchResults.Should().HaveCount(3);

        var results = summary.MatchResults.ToList();

        results[0].HomeTeam.Should().Be("Team C");
        results[0].AwayTeam.Should().Be("Team D");
        results[0].HomeScore.Should().Be(2);
        results[0].AwayScore.Should().Be(2);

        results[1].HomeTeam.Should().Be("Team E");
        results[1].AwayTeam.Should().Be("Team F");
        results[1].HomeScore.Should().Be(1);
        results[1].AwayScore.Should().Be(1);

        results[2].HomeTeam.Should().Be("Team A");
        results[2].AwayTeam.Should().Be("Team B");
        results[2].HomeScore.Should().Be(1);
        results[2].AwayScore.Should().Be(0);
    }

    [Fact]
    public void UpdateScore_ShouldThrowExceptionWhenMatchNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        Action act = () => _scoreboard.UpdateScore(guid, 1, 1);

        // Assert
        act.Should().Throw<KeyNotFoundException>().WithMessage($"No match found with ID {guid}.");
    }

    [Fact]
    public void FinishMatch_ShouldThrowExceptionWhenMatchNotFound()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        Action act = () => _scoreboard.FinishMatch(guid);

        // Assert
        act.Should().Throw<KeyNotFoundException>().WithMessage($"No match found with ID {guid}.");
    }

    [Fact]
    public void StartMatch_ShouldThrowExceptionWhenTeamNamesAreInvalid()
    {
        // Act
        Action act1 = () => _scoreboard.StartMatch("", "Team B");
        Action act2 = () => _scoreboard.StartMatch("Team A", "");
        Action act3 = () => _scoreboard.StartMatch("Team A", "Team A");

        // Assert
        act1.Should().Throw<ArgumentException>().WithMessage("Team names cannot be null or empty. (Parameter 'homeTeam')");
        act2.Should().Throw<ArgumentException>().WithMessage("Team names cannot be null or empty. (Parameter 'awayTeam')");
        act3.Should().Throw<ArgumentException>().WithMessage("A team cannot play against itself.");
    }

    [Fact]
    public void StartMatch_ShouldThrowExceptionWhenTeamsAreAlreadyPlaying()
    {
        // Arrange
        _scoreboard.StartMatch("Team A", "Team B");
        _scoreboard.StartMatch("Team C", "Team D");


        // Act
        Action act = () => _scoreboard.StartMatch("Team A", "Team E");
        Action act2 = () => _scoreboard.StartMatch("Team E", "Team D");
        Action act3 = () => _scoreboard.StartMatch("Team A", "Team C");

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Home team is already playing a match. (Parameter 'homeTeam')");
        act2.Should().Throw<ArgumentException>().WithMessage("Away team is already playing a match. (Parameter 'awayTeam')");
        act3.Should().Throw<ArgumentException>().WithMessage("Both teams are already playing a match.");
    }

    [Fact]
    public void UpdateScore_ShouldThrowExceptionWhenScoresAreNegative()
    {
        // Arrange
        var matchId = _scoreboard.StartMatch("Team A", "Team B");

        // Act
        Action act = () => _scoreboard.UpdateScore(matchId, -1, 2);
        Action act2 = () => _scoreboard.UpdateScore(matchId, 1, -2);
        Action act3 = () => _scoreboard.UpdateScore(matchId, -1, -2);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Scores must be non-negative.");
        act2.Should().Throw<ArgumentException>().WithMessage("Scores must be non-negative.");
        act3.Should().Throw<ArgumentException>().WithMessage("Scores must be non-negative.");
    }

    [Fact]
    public void UpdateScore_ByTeamNamesShouldUpdateMatchScores()
    {
        // Arrange
        _scoreboard.StartMatch("Team A", "Team B");

        // Act
        _scoreboard.UpdateScore("Team A", "Team B", 3, 2);

        // Assert
        var summary = _scoreboard.GetSummary();
        summary.MatchResults.Should().ContainSingle(match =>
            match.HomeTeam == "Team A" &&
            match.AwayTeam == "Team B" &&
            match.HomeScore == 3 &&
            match.AwayScore == 2);
    }

    [Fact]
    public void FinishMatch_ByTeamNamesShouldRemoveMatchFromScoreboard()
    {
        // Arrange
        _scoreboard.StartMatch("Team A", "Team B");

        // Act
        _scoreboard.FinishMatch("Team A", "Team B");

        // Assert
        var summary = _scoreboard.GetSummary();
        summary.MatchResults.Should().BeEmpty();
    }

    [Fact]
    public void UpdateScore_ByTeamNamesShouldThrowExceptionWhenMatchNotFound()
    {
        // Arrange
        _scoreboard.StartMatch("Team A", "Team B");

        // Act
        Action act = () => _scoreboard.UpdateScore("Nonexistent Team A", "Nonexistent Team B", 1, 1);
        Action act2 = () => _scoreboard.UpdateScore("", "Team B", 1, 1);
        Action act3 = () => _scoreboard.UpdateScore("Team A", "", 1, 1);
        Action act4 = () => _scoreboard.UpdateScore("Team A", "Team A", 1, 1);


        // Assert
        act.Should().Throw<KeyNotFoundException>().WithMessage("No match found for teams Nonexistent Team A vs Nonexistent Team B.");
        act2.Should().Throw<ArgumentException>().WithMessage("Team names cannot be null or empty. (Parameter 'homeTeam')");
        act3.Should().Throw<ArgumentException>().WithMessage("Team names cannot be null or empty. (Parameter 'awayTeam')");
        act4.Should().Throw<ArgumentException>().WithMessage("A team cannot play against itself.");
    }

    [Fact]
    public void FinishMatch_ByTeamNamesShouldThrowExceptionWhenMatchNotFound()
    {
        // Act
        Action act = () => _scoreboard.FinishMatch("Nonexistent Team A", "Nonexistent Team B");

        // Assert
        act.Should().Throw<KeyNotFoundException>().WithMessage("No match found for teams Nonexistent Team A vs Nonexistent Team B.");
    }
}
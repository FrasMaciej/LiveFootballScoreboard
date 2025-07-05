using LiveFootballScoreboard.Contracts;

namespace LiveFootballScoreboard.Tests;

public class ScoreboardFactoryTests
{
    [Fact]
    public void Create_ShouldReturnIScoreboardInstance()
    {
        // Act
        var scoreboard = ScoreboardFactory.Create();

        // Assert
        Assert.NotNull(scoreboard);
        Assert.IsAssignableFrom<IScoreboard>(scoreboard);
    }
}
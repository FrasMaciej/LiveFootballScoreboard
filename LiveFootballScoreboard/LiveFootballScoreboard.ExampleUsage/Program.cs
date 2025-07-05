using LiveFootballScoreboard.Contracts;

IScoreboard scoreboard = ScoreboardFactory.Create();

Guid match1Id = scoreboard.StartMatch("Mexico", "Canada");
scoreboard.StartMatch("Spain", "Brazil");
Guid match3Id = scoreboard.StartMatch("Germany", "France");
scoreboard.StartMatch("Uruguay", "Italy");
Guid match5Id = scoreboard.StartMatch("Argentina", "Australia");

try
{
    scoreboard.StartMatch("Mexico", "Poland");
}
catch (Exception e)
{
    Console.WriteLine(e);
}

scoreboard.UpdateScore(match1Id, 0, 5);
scoreboard.UpdateScore("Spain", "Brazil", 10, 2);
scoreboard.UpdateScore(match3Id, 2, 2);
scoreboard.UpdateScore("Uruguay", "Italy", 6, 6);
scoreboard.UpdateScore(match5Id, 3, 1);

var summary = scoreboard.GetSummary();

Console.WriteLine("\nMatch Summary:");
PrintSummary(summary);

scoreboard.FinishMatch("Germany", "France");
var summary2 = scoreboard.GetSummary();
Console.WriteLine("\nMatch Summary after removing one match:");
PrintSummary(summary2);

scoreboard.StartMatch("Poland", "Slovakia");
var summary3 = scoreboard.GetSummary();
Console.WriteLine("\nMatch Summary after adding new match:");
PrintSummary(summary3);

static void PrintSummary(MatchesSummary summary)
{
    foreach (var match in summary.MatchResults)
    {
        Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
    }
}
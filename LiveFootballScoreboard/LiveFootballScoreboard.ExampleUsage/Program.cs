using LiveFootballScoreboard.Contracts;

IScoreboard scoreboard = ScoreboardFactory.Create();

Guid match1Id = scoreboard.StartMatch("Mexico", "Canada");
Guid match2Id = scoreboard.StartMatch("Spain", "Brazil");
Guid match3Id = scoreboard.StartMatch("Germany", "France");
Guid match4Id = scoreboard.StartMatch("Uruguay", "Italy");
Guid match5Id = scoreboard.StartMatch("Argentina", "Australia");

scoreboard.UpdateScore(match1Id, 0, 5);
scoreboard.UpdateScore(match2Id, 10, 2);
scoreboard.UpdateScore(match3Id, 2, 2);
scoreboard.UpdateScore(match4Id, 6, 6);
scoreboard.UpdateScore(match5Id, 3, 1);

var summary = scoreboard.GetSummary();
Console.WriteLine("\nMatch Summary:");
foreach (var match in summary.MatchResults)
{
    Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
}

scoreboard.FinishMatch(match3Id);
Console.WriteLine("\nMatch Summary after removing one match:");
foreach (var match in summary.MatchResults)
{
    Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
}

scoreboard.StartMatch("Poland", "Slovakia");
Console.WriteLine("\nMatch Summary after adding new match:");
foreach (var match in summary.MatchResults)
{
    Console.WriteLine($"{match.HomeTeam} {match.HomeScore} - {match.AwayTeam} {match.AwayScore}");
}
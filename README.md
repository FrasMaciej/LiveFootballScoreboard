# Live Football Score Board Documentation

The library consists of three parts:

1. Project building guide
2. User API Reference
3. Library implementation description

# 1. Project building guide

To test library you need to use .NET 8 or newer

1. Clone the repository.
2. Build the solution to restore dependencies (using dotnet CLI or your IDE like Visual Studio or Rider)
3. Run the tests to verify the implementation.
4. Play with the library using provided additional console application

Detailed commands to Build and test with CLI (execute from main project directory):
1. dotnet build LiveFootballScoreboard.sln
2. dotnet test LiveFootballScoreboard.Tests/LiveFootballScoreboard.Tests.csproj
3. dotnet run --project LiveFootballScoreboard.ExampleUsage/LiveFootballScoreboard.ExampleUsage.csproj

To generate tests report (execute from Tests directory):
1. dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults
2. reportgenerator -reports:./TestResults/**/coverage.cobertura.xml -targetdir:./CoverageReport -reporttypes:Html
3. report is available in LiveFootballScoreboard.Tests/CoverageReport/index.html

# 2. User Api Reference

## Project Overview

The Live Football World Cup Scoreboard library is a simple in-memory solution designed to manage and display live football match scores. It adheres to the principles of simplicity, quality, and clean code.

## Features

The library supports the following operations:

1. **Start a New Match**
   - Adds a new match to the scoreboard with an initial score of 0-0.

2. **Update Score**
   - Updates the score of an ongoing match with absolute scores for both teams (using Id of match or pair of team names for specyfying it):

3. **Finish Match**
   - Removes a match currently in progress from the scoreboard using the match Id or teams name.

4. **Get Summary of Matches**
   - Retrieves a summary of all matches in progress, ordered by:
     - Total score (highest to lowest)
     - Most recently started match in case of ties.


### Example Summary

If the following matches are started and updated in the specified order:

- Mexico 0 - Canada 5
- Spain 10 - Brazil 2
- Germany 2 - France 2
- Uruguay 6 - Italy 6
- Argentina 3 - Australia 1

The summary will be:

1. Uruguay 6 - Italy 6
2. Spain 10 - Brazil 2
3. Mexico 0 - Canada 5
4. Argentina 3 - Australia 1
5. Germany 2 - France 2

### API Details

The following APIs are exposed to the user through the `Contracts` directory:

#### `IScoreboard`
This interface defines the operations supported by the scoreboard.

- **Methods:**

1. `void StartMatch(string homeTeam, string awayTeam)`
   - **Description:** Starts a new match with the given home and away teams. The initial score is set to 0-0.
   - **Parameters:**
      - `homeTeam` (string): Name of the home team.
      - `awayTeam` (string): Name of the away team.
   - **Returns:** None
2. `void UpdateScore(Guid matchId, int homeScore, int awayScore)`
   - **Description:** Updates the score of an ongoing match.
   - **Parameters:**
      - `matchId` (Guid): Unique identifier of the match.
      - `homeScore` (int): Absolute score of the home team.
      - `awayScore` (int): Absolute score of the away team.
   - **Returns:** None
3. `void UpdateScore(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)`
   - **Description:** Updates the score of an ongoing match.
   - **Parameters:**
      - `homeTeam` (string): Name of the home team.
      - `awayTeam` (string): Name of the away team.
      - `homeTeamScore` (int): Absolute score of the home team.
      - `awayTeamScore` (int): Absolute score of the away team.
   - **Returns:** None
4. `void FinishMatch(Guid matchId)`
   - **Description:** Finishes a match and removes it from the scoreboard.
   - **Parameters:**
      - `matchId` (Guid): Unique identifier of the match.
   - **Returns:** None
5. `void FinishMatch(string homeTeam, string awayTeam)`
    - **Description:** Finishes a match and removes it from the scoreboard.
   - **Parameters:**
      - `homeTeam` (string): Name of the home team.
      - `awayTeam` (string): Name of the away team.
   - **Returns:** None
6. `IEnumerable<MatchesSummary> GetSummary()`
   - **Description:** Retrieves a summary of all matches in progress, ordered by total score and start time.
   - **Parameters:** None
   - **Returns:** A collection of `MatchesSummary` objects.

#### `MatchesSummary`
This class provides a summary of matches.

- **Properties:**
   - `string HomeTeam` (string): Name of the home team.
   - `string AwayTeam` (string): Name of the away team.
   - `int HomeScore` (int): Current score of the home team.
   - `int AwayScore` (int): Current score of the away team.
   - `int TotalScore` (int): Total score of the match (sum of home and away scores).

#### `MatchResult`
This class represents the result of a match.

- **Properties:**
   - `Guid MatchId` (Guid): Unique identifier of the match.
   - `string HomeTeam` (string): Name of the home team.
   - `string AwayTeam` (string): Name of the away team.
   - `int HomeScore` (int): Final score of the home team.
   - `int AwayScore` (int): Final score of the away team.

#### `ScoreboardFactory`
This factory class is used to create instances of the scoreboard.

- **Methods:**
   1. `IScoreboard CreateScoreboard()`
      - **Description:** Creates a new instance of the scoreboard.
      - **Parameters:** None
      - **Returns:** An instance of `IScoreboard`.

# 3. Library implementation description

## Design and Implementation

The provided solution consists of:
1. LiveFootballScoreboard - contains ready-to-use library 
2. LiveFootballScoreboard.Tests - unit tests of library 
3. LiveFootballScoreboard.ExampleUsage - simple console application that lets to do some manual tests of library

### Key Components of the library

1. **Models** 
   Module that represents data models used inside library (Internal Visibility)
   - `Match`: Represents a football match with details such as teams and scores.
   - `Team`: Represents a football team.

2. **Contracts**
   Module that contains all of the abstraction layer that can be used by user of the library. (Public Visibility)
   - `IScoreboard`: Interface defining the operations supported by the scoreboard.
   - `MatchesSummary`: Model of summary of matches.
   - `MatchResult`: Model of the result of a match.
   - `ScoreboardFactory`: Factory class to create instances of the scoreboard.

3. **Services**
   Module that is responsible for core operations supported by the library (Internal Visiblity) 
   - `ScoreboardService`: Implements the `IScoreboard` interface and contains the core logic of the library.

### Assumptions

- The library assumes that all matches are unique based on the combination of home and away teams. The library does not allow to operate on one team with specified name in more than one match. If user will try to for example add new match with team name that already exists an exception will be thrown. 
- The library does not persist data; it is designed to work entirely in memory.
- The ordering of matches in the summary is based on the total score and the time of addition to the scoreboard.

## Testing

The library includes unit tests to ensure correctness and reliability. The tests are implemented in 
- `ScoreboardFactoryTests.cs`
- `ScoreboardServiceTests.cs`

## Explanation of crucial decisions in the project

- The library is using the Id to identify matches. It makes the library more error-resistant and improves the efficiency. From the user perspective there are two ways to manipulate matches: by id and by team names.
- The library is using Dictionary to store matches, where Id of the match is the key and Match is a value. It was the most effective way I found to store and manipulate the data. There was only one tradeoff in StartMatch method: when adding new match, the method have to first flatten all of the values of team names, so it can validate if the specified team already exists. In my opinion that was a good tradeoff, as we can expect that the frequency of adding new matches will be much lower than the frequency of modyfing/removing matches -> the other option would be using separate Set for storing team names but it would increase complexity significantly.
- I was focused on making the implementation as simple as it can be following the KISS OOP principle, but still the application has separate interfaces and models that are exposed to user. With that, even if the implementation would change much under the hood, it's still possible to maintain easly the existing API or just provide new API in clean way.
- I decided to not use any external packages for building the library, because it was not necessary in that kind of project. I could easly build it with everything that comes with standard libraries; Adding external dependencies would not bring significant gains in my opinion (tests are using FluentValidation for better readability and coverlet.collector for generating tests report)
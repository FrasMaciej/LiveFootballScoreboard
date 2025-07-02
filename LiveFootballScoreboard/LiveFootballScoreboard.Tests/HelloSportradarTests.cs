namespace LiveFootballScoreboard.Tests;

public class HelloSportradarTests
{
    [Fact]
    public void SayHello_ReturnsHelloSportradar()
    {
        // Arrange
        var helloSportradar = new HelloSportradar();
        // Act
        var result = helloSportradar.SayHello();
        // Assert
        Assert.Equal("Hello Sportradar!", result);
    }

}
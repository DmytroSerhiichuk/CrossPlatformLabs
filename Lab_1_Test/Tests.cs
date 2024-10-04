using Lab_1;
using Xunit;
using Xunit.Abstractions;
namespace Lab_1_Test;

public class Tests
{
    private readonly ITestOutputHelper _output;

    public Tests(ITestOutputHelper output)
    {
        _output = output;

        SetCurrentDirectory();
    }

    static void SetCurrentDirectory()
    {
        var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(projectRoot, @"../../../")));
    }

    [Theory]
    [InlineData("INPUT 1.txt", 64)]
    [InlineData("INPUT 2.txt", 1)]
    [InlineData("INPUT 3.txt", 0)]
    [InlineData("INPUT 4.txt", 4)]
    [InlineData("INPUT 5.txt", 256)]
    public void GetResultFromFile(string filePath, int expectedCount)
    {
        (var p1, var p2) = Program.ReadInput(Path.Combine("TestInputs", filePath));

        Program.CheckInputs(p1, p2);

        var res = Program.CalcCount(p1, p2);

        Assert.Equal(expectedCount, res);
        _output.WriteLine($"Result: {res}");
    }
}
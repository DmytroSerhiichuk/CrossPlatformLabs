using Lab_2;
using Xunit;
using Xunit.Abstractions;

namespace Lab_2_Test;

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
    [InlineData(2, 2)]      // 1 (2) 3 4 5 6 8 9 10 12 15 16 18 20 24 25 27 30...
    [InlineData(9, 10)]     // 1 2 3 4 5 6 8 9 (10) 12 15 16 18 20 24 25 27 30...
    [InlineData(8, 9)]      // 1 2 3 4 5 6 8 (9) 10 12 15 16 18 20 24 25 27 30...
    [InlineData(16, 25)]    // 1 2 3 4 5 6 8 9 10 12 15 16 18 20 24 (25) 27 30...
    [InlineData(11, 15)]    // 1 2 3 4 5 6 8 9 10 12 (15) 16 18 20 24 25 27 30...
    public void GetResult_ReturnsCorrectCount(int n, ulong expectedResult)
    {
        var res = Program.GetResult(n);

        Assert.Equal(expectedResult, res);
        _output.WriteLine($"n: {n} | expected: {expectedResult} | Result: {res}");
    }

    [Theory]
    [InlineData("")]
    [InlineData("ac")]
    [InlineData("nine")]
    [InlineData("-5")]
    [InlineData("0")]
    [InlineData("10005")]
    public void CheckInput_ShouldThrowArgumentException_ForInvalidData(string str)
    {
        var exception = Assert.Throws<ArgumentException>(() => Program.CheckInput(str));

        _output.WriteLine($"input: \"{str}\" | message: {exception.Message}");
    }
}
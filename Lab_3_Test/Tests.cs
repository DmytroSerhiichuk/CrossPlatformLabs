using Lab_3;
using Xunit;
using Xunit.Abstractions;

namespace Lab_3_Test;

public class Tests
{
    private readonly ITestOutputHelper _output;

    public static IEnumerable<object[]> ArgumentExceptionInputs =>
        new List<object[]>
        {
            new object[] { new string[] { "Hello", "World" } },
            new object[] { new string[] { "1", "2", "4", "2", "12", "1" } },
            new object[] { new string[] { "10", "one", "2", "2", "5" } }
        };

    public static IEnumerable<object[]> ArgumentOutOfRangeExceptionInputs =>
    new List<object[]>
    {
            new object[] { new string[] { "1", "2", "2", "3", "4" } },
            new object[] { new string[] { "50", "12", "2", "3", "7" } },
            new object[] { new string[] { "8", "-2", "4", "1", "4" } },
            new object[] { new string[] { "8", "1", "15", "7", "6" } },
    };

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
    [MemberData(nameof(ArgumentExceptionInputs))]
    public void InitInput_ShouldThrowArgumentException_ForInvalidData(string[] inputs)
    {
        var exception = Assert.Throws<ArgumentException>(() => Program.InitInputs(inputs));

        var inputsStr = "";

        foreach (var i in inputs)
        {
            inputsStr += $" {i}";
        }

        _output.WriteLine($"input:{inputsStr} | message: {exception.Message}");
    }

    [Theory]
    [MemberData(nameof(ArgumentOutOfRangeExceptionInputs))]
    public void InitInput_ShouldThrowArgumentOutOfRangeException_ForInvalidData(string[] inputs)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Program.InitInputs(inputs));

        var inputsStr = "";

        foreach (var i in inputs)
        {
            inputsStr += $" {i}";
        }

        _output.WriteLine($"input:{inputsStr} | message: {exception.Message}");
    }

    [Theory]
    [InlineData(5, 1, 1, 3, 1, 2)]
    [InlineData(8, 5, 5, 4, 5, 3)]
    [InlineData(7, 2, 2, 4, 4, 4)]
    public void GetResult_ReturnsCorrectCount(uint N, int x1, int y1, int x2, int y2, int expected)
    {
        var start = new Point(x1 - 1, y1 - 1);
        var end = new Point(x2 - 1, y2 - 1);

        var res = Program.GetResult(N, start, end);

        Assert.Equal(expected, res);
        _output.WriteLine($"N: {N} | Start: {start} | End: {end} | expected: {expected} | Result: {res}");
    }
}
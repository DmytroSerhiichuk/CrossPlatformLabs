using Lab_4;
using Xunit;
using Xunit.Abstractions;

namespace Lab_4_Test;

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
    [InlineData("run lab4", HandleInputsResult.SyntaxError)]
    [InlineData("set-path", HandleInputsResult.SyntaxError)]
    [InlineData("run lab1 --i .\\INPUT.txt", HandleInputsResult.SyntaxError)]
    [InlineData("run lab1 -p .\\INPUT.txt", HandleInputsResult.SyntaxError)]
    public void HandleInputs_ReturnsSyntaxError(string input, HandleInputsResult expected)
    {
        var res = Program.HandleInputs(input);

        Assert.Equal(expected, res);
        _output.WriteLine($"input: {input} | expected: {expected} | result: {res}");
    }

    [Theory]
    [InlineData("version", HandleInputsResult.Success)]
    [InlineData("set-path -p .", HandleInputsResult.Success)]
    public void HandleInputs_ReturnsSuccess(string input, HandleInputsResult expected)
    {
        var res = Program.HandleInputs(input);

        Assert.Equal(expected, res);
        _output.WriteLine($"input: {input} | expected: {expected} | result: {res}");
    }
}
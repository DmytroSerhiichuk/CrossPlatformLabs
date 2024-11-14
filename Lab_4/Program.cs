using System.Text.RegularExpressions;

namespace Lab_4;

public class Program
{
    public static readonly List<(string Command, string Description)> Commands = new()
    {
        ("version", "show info"),
        ("set-path [-p | --path] <path to folder>", "set path to folder with input file"),
        ("run lab{1|2|3} [-i | --input] [<path to input>] [-o | --output] [<path to output>]", "run a specific project")
    };
    public static readonly int MaxCommandWidth;

    static Program()
    {
        MaxCommandWidth = 0;
        foreach (var cmd in Commands)
        {
            MaxCommandWidth = Math.Max(MaxCommandWidth, cmd.Command.Length);
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Start\n");
        if (args.Length == 0)
        {
            Help();
        }
        else
        {
            var res = HandleInputs(args);
            if (res == HandleInputsResult.SyntaxError)
            {
                Console.WriteLine("Invalid command!");
                Help();
            }
        }
    }

    public static HandleInputsResult HandleInputs(string[] inputs)
    {
        var input = String.Join(' ', inputs);

        Match match;

        if (inputs.Length > 0)
        {
            if (inputs[0] == "version" && inputs.Length == 1)
            {
                Console.WriteLine("Author: Dmytro Serhiichuk");
                Console.WriteLine("Version: 1.0.0");

                return HandleInputsResult.Success;
            }
            else if (TryMatch(input, "^set-path (?:-p|--path)\\s+(\"[^\"]+\"|\\S+)$", out match))
            {
                var path = match.Groups[1].Value;
                if (Directory.Exists(path))
                {
                    Environment.SetEnvironmentVariable("LAB_PATH", path);
                    Console.WriteLine("Success: Directory path updated");

                    return HandleInputsResult.Success;
                }
                else
                {
                    Console.WriteLine($"Error: Directory not found!");

                    return HandleInputsResult.DirectoryNotFound;
                }
            }
            else if (TryMatch(input, "^run (lab[123])(?:\\s+(?:-i|--input)\\s+(\"[^\"]+\"|\\S+))?(?:\\s+(?:-o|--output)\\s+(\"[^\"]+\"|\\S+))?$", out match))
            {
                var lab = match.Groups[1].Value;
                var inputFilePath = match.Groups[2].Success && File.Exists(match.Groups[2].Value) ? match.Groups[2].Value : String.Empty;
                var outputFilePath = match.Groups[3].Success && File.Exists(match.Groups[3].Value) ? match.Groups[3].Value : String.Empty;

                if (inputFilePath == String.Empty)
                {
                    var dir = Environment.GetEnvironmentVariable("LAB_PATH");

                    if (dir != null)
                    {
                        var file = Path.Combine(dir, "INPUT.txt");
                        if (File.Exists(file))
                        {
                            inputFilePath = file;
                        }
                    }
                }
                if (inputFilePath == String.Empty)
                {
                    var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    var file = Path.Combine(dirPath, "INPUT.txt");

                    if (File.Exists(file))
                    {
                        inputFilePath = file;
                    }
                }
                if (inputFilePath == String.Empty)
                {
                    Console.WriteLine("Error: Input file not found");
                    return HandleInputsResult.InputNotFound;
                }

                if (outputFilePath == String.Empty)
                {
                    outputFilePath = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(inputFilePath)), "OUTPUT.txt");
                }

                var labNum = int.Parse(lab[lab.Length - 1].ToString());

                try
                {
                    if (labNum == 1)
                    {
                        var labObj = new LabLib.Lab_1(inputFilePath);
                        var res = labObj.Do();
                        labObj.SaveOutput(res, outputFilePath);
                    }
                    else if (labNum == 2)
                    {
                        var labObj = new LabLib.Lab_2(inputFilePath);
                        var res = labObj.Do();
                        labObj.SaveOutput(res, outputFilePath);
                    }
                    else
                    {
                        var labObj = new LabLib.Lab_3(inputFilePath);
                        var res = labObj.Do();
                        labObj.SaveOutput(res, outputFilePath);
                    }

                    return HandleInputsResult.Success;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid INPUT data!");

                    return HandleInputsResult.InvalidInput;
                }
            }
        }

        return HandleInputsResult.SyntaxError;
    }

    static void Help()
    {
        Console.WriteLine("Avaible commands:\n");

        foreach (var (Command, Description) in Commands)
        {
            Console.WriteLine($"  {Command.PadRight(MaxCommandWidth)} {Description}");
        }
        Console.WriteLine();
    }

    static bool TryMatch(string input, string pattern, out Match match)
    {
        match = Regex.Match(input, pattern);
        return match.Success;
    }
}

public enum HandleInputsResult
{
    Success,
    SyntaxError,
    InputNotFound,
    InvalidInput,
    DirectoryNotFound
}
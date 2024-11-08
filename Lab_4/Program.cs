using System.Text.RegularExpressions;

namespace Lab_4;

public class Program
{
    public static readonly List<(string Command, string Description)> Commands = new()
    {
        ("version", "показати автора та версію"),
        ("set-path [-p | --path] <шлях до папки>", "встановити шлях до папки з input та output файлами"),
        ("run lab{1|2|3} [-i | --input] [<шлях до input файла>] [-o | --output] [<шлях до output файла>]", "запустити лабораторну роботу")
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
        Console.WriteLine("Start");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            HandleInputs(input);
        }
    }

    static void HandleInputs(string input)
    {
        var inputs = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Match match;

        if (inputs.Length > 0)
        {
            if (inputs[0] == "version" && inputs.Length == 1)
            {
                Console.WriteLine("Author: Dmytro Serhiichuk");
                Console.WriteLine("Version: 1.0.0");
            }
            else if (TryMatch(input, "^set-path (?:-p|--path)\\s+(\"[^\"]+\"|\\S+)$", out match))
            {
                var path = match.Groups[1].Value;
                if (Directory.Exists(path))
                {
                    Environment.SetEnvironmentVariable("LAB_PATH", path);
                    Console.WriteLine("Success: Directory path updated");
                }
                else
                {
                    Console.WriteLine($"Error: Directory not found!");
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
                    return;
                }

                if (outputFilePath == String.Empty)
                {
                    outputFilePath = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(inputFilePath)), "OUTPUT.txt");
                }

                var labNum = int.Parse(lab[lab.Length - 1].ToString());

                try
                {
                    Action<string, string> labAction = labNum switch
                    {
                        1 => Lab_4_LabLib.Lab_1.Do,
                        2 => Lab_4_LabLib.Lab_2.Do,
                        _ => Lab_4_LabLib.Lab_3.Do
                    };

                    labAction(inputFilePath, outputFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid INPUT data!");
                }
            }
            else
            {
                Help();
            }
        }
        else
        {
            Help();
        }
    }

    static void Help()
    {
        Console.WriteLine("Некоректна команда!\n" +
            "Список команд:\n");

        foreach (var (Command, Description) in Commands)
        {
            Console.WriteLine($" {Command.PadRight(MaxCommandWidth)} {Description}");
        }
        Console.WriteLine();
    }

    static bool TryMatch(string input, string pattern, out Match match)
    {
        match = Regex.Match(input, pattern);
        return match.Success;
    }

    private static string GetFilePath(string path)
    {
        return !string.IsNullOrEmpty(path) && File.Exists(path) ? path : null;
    }
}
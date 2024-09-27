using Microsoft.VisualBasic;

namespace Lab_1;

class Program
{
    const string ValidChars = "0123456789abcdef?";
    readonly static Dictionary<char, string> Replacements = new Dictionary<char, string>
    {
        {'?', "0123456789"},
        {'a', "0123"},
        {'b', "1234"},
        {'c', "2345"},
        {'d', "3456"},
        {'e', "4567"},
        {'f', "5678"},
        {'g', "6789"},
    };

    static void Main(string[] args)
    {
        (var p1, var p2) = ReadInput();

        Console.WriteLine($"p1 = {p1}");
        Console.WriteLine($"p2 = {p2}");

        var S1 = new HashSet<string>();
        GenerateCombinations(p1, S1);

        var S2 = new HashSet<string>();
        GenerateCombinations(p2, S2);

        var res = new HashSet<string>(S1.Count + S2.Count);
        foreach (var item in S1)
        {
            if (S2.Contains(item))
            {
                res.Add(item);
            }
        }

        Console.WriteLine($"Count: {res.Count}");
        foreach (var i in res)
        {
            Console.WriteLine(i);
        }

        SaveOutput(res.Count);
    }

    static (string, string) ReadInput()
    {
        using (var sr = new StreamReader("./INPUT.txt"))
        {
            var p1 = sr.ReadLine().Trim();
            var p2 = sr.ReadLine().Trim();

            CheckInputs(p1, p2);

            return (p1, p2);
        }
    }
    static void CheckInputs(string p1, string p2)
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("Вхідні дані мають бути однакової довжини");
        }

        if (p1.Length == 0 || p1.Length > 9 || p2.Length == 0 || p2.Length > 9)
        {
            throw new ArgumentException("Вхідні дані мають бути довжиною від 1 до 9");
        }

        if (p1.Any(c => !ValidChars.Contains(c)) || p2.Any(c => !ValidChars.Contains(c)))
        {
            throw new ArgumentException($"Допустимі символи: {string.Join(", ", ValidChars.ToCharArray())}");
        }
    }

    static void GenerateCombinations(string input, HashSet<string> results, int index = 0, string current = "")
    {
        if (index == input.Length)
        {
            results.Add(current);
            return;
        }

        char currentChar = input[index];

        if (Replacements.TryGetValue(currentChar, out string replacement))
        {
            foreach (char replacementChar in replacement)
            {
                GenerateCombinations(input, results, index + 1, current + replacementChar);
            }
        }
        else
        {
            GenerateCombinations(input, results, index + 1, current + currentChar);
        }
    }

    static void SaveOutput(int count)
    {
        using (var sw = new StreamWriter("OUTPUT.txt"))
        {
            sw.WriteLine(count);
        }
    }
}
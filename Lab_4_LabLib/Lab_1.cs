namespace Lab_4_LabLib;

public class Lab_1
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

    public static void Do(string inputPath, string outputPath)
    {
        (var p1, var p2) = ReadInput(inputPath);

        CheckInputs(p1, p2);

        Console.WriteLine($"p1 = {p1}");
        Console.WriteLine($"p2 = {p2}");

        var res = CalcCount(p1, p2);

        Console.WriteLine($"Count: {res}");

        SaveOutput(res, outputPath);
    }

    static (string, string) ReadInput(string path)
    {
        using (var sr = new StreamReader(path))
        {
            var p1 = sr.ReadLine()?.Trim();
            var p2 = sr.ReadLine()?.Trim();

            if (p1 == null)
            {
                var paramName = nameof(p1);
                throw new ArgumentNullException(paramName);
            }
            if (p2 == null)
            {
                var paramName = nameof(p2);
                throw new ArgumentNullException(paramName);
            }

            return (p1, p2);
        }
    }
    static void CheckInputs(string p1, string p2)
    {
        if (p1.Length != p2.Length)
        {
            throw new ArgumentException("The input data must be of the same length");
        }

        if (p1.Length == 0 || p1.Length > 9 || p2.Length == 0 || p2.Length > 9)
        {
            throw new ArgumentException("Input data must be between 1 and 9 in length");
        }

        if (p1.Any(c => !ValidChars.Contains(c)) || p2.Any(c => !ValidChars.Contains(c)))
        {
            throw new ArgumentException($"Valid characters: {string.Join(", ", ValidChars.ToCharArray())}");
        }
    }

    static int CalcCount(string p1, string p2)
    {
        var res = 0;

        for (var i = 0; i < p1.Length; i++)
        {
            string t1 = p1[i].ToString(), t2 = p2[i].ToString();

            if (p1[i] < '0' || p1[i] > '9')
            {
                t1 = Replacements[p1[i]];
            }
            if (p2[i] < '0' || p2[i] > '9')
            {
                t2 = Replacements[p2[i]];
            }

            var count = t1.Count(c => t2.Contains(c));

            if (count > 0 && res == 0)
            {
                res = count;
            }
            else if (count > 0)
            {
                res *= count;
            }
            else
            {
                res = 0;
                break;
            }
        }

        return res;
    }

    static void SaveOutput(int count, string outputPath)
    {
        using (var sw = new StreamWriter(outputPath))
        {
            sw.WriteLine(count);
        }
    }
}
namespace Lab_2;

public class Program
{
    static void Main(string[] args)
    {
        var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(projectRoot, @"../../../")));

        var nStr = ReadFile("INPUT.txt");

        var n = InitInput(nStr);

        Console.WriteLine($"N: {n}");

        var output = GetResult((int)n);

        Console.WriteLine($"Result: {output}");

        SaveOutput(output);
    }

    public static string ReadFile(string path)
    {
        using (var sr = new StreamReader(path))
        {
            var str = sr.ReadLine()?.Trim();

            if (str == null)
            {
                var paramName = nameof(str);
                throw new ArgumentNullException(paramName);
            }

            return str;
        }
    }

    public static uint InitInput(string input)
    {
        if (!uint.TryParse(input, out var n))
        {
            throw new ArgumentException("Input data must be the unsigned integer");
        }

        if (n <= 0 || n > 10000)
        {
            throw new ArgumentException("N must be in the range from 1 to 10000");
        }

        return n;
    }

    public static ulong GetResult(int n)
    {
        if (n > 10000 || n <= 0)
        {
            throw new ArgumentException("N must be in the range from 1 to 10000");
        }

        var lastAdded = new List<ulong>(9) { 1 };

        var maxIteration = (int)Math.Ceiling(Math.Sqrt(n));

        var resList = new List<ulong>(maxIteration) { 1 };

        for (var i = 0; i < maxIteration; i++)
        {
            lastAdded = Add(lastAdded, resList);
        }

        resList.Sort();
        resList = resList.Where(x => x > 0).Take(n).ToList();

        return resList[n - 1];
    }

    static List<ulong> Add(List<ulong> lastAdded, List<ulong> resList)
    {
        var data = new List<ulong>(9);

        foreach (var item in lastAdded)
        {
            var i2 = item * 2;
            var i3 = item * 3;
            var i5 = item * 5;

            if (!data.Contains(i2))
            {
                data.Add(i2);
            }
            if (!data.Contains(i3))
            {
                data.Add(i3);
            }
            if (!data.Contains(i5))
            {
                data.Add(i5);
            }
        }

        resList.AddRange(data);

        return data;
    }

    public static void SaveOutput(ulong output)
    {
        using (var sw = new StreamWriter("OUTPUT.txt"))
        {
            sw.WriteLine(output);
        }
    }
}

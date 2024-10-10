namespace Lab_3;
public class Program
{
    public static readonly Point[] Moves = new Point[8]
    {
        new Point(-2, -1), new Point(-1, -2),
        new Point(1, -2), new Point(2, -1),
        new Point(2, 1), new Point(1, 2),
        new Point(-1, 2), new Point(-2, 1)
    };

    static void Main(string[] args)
    {
        var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(Path.GetFullPath(Path.Combine(projectRoot, @"../../../")));

        var inputs = ReadInput("INPUT.txt");

        (var N, var start, var end) = InitInputs(inputs);

        Console.WriteLine($"N: {N}");
        Console.WriteLine($"Start: {start}");
        Console.WriteLine($"End: {end}\n");

        var res = GetResult(N, start, end);

        Console.WriteLine($"Result: {res}");

        SaveOutput(res);
    }

    public static string[] ReadInput(string file)
    {
        using (var sr = new StreamReader(file))
        {
            var line = sr.ReadLine()?.Trim();

            if (line == null)
            {
                var paramName = nameof(line);
                throw new ArgumentNullException(paramName);
            }

            var inputs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return inputs;
        }
    }

    public static (uint, Point, Point) InitInputs(string[] inputs)
    {
        if (inputs.Length != 5)
            throw new ArgumentException("Input data must consist of 5 elements");

        if (!uint.TryParse(inputs[0], out var N))
            throw new ArgumentException("Can't convert N to uint");
        if (N < 5 || N > 20)
            throw new ArgumentOutOfRangeException("N must be in the range from 5 to 20");

        if (!int.TryParse(inputs[1], out var x1))
            throw new ArgumentException("Can't convert x1 to int");
        if (!int.TryParse(inputs[2], out var y1))
            throw new ArgumentException("Can't convert y1 to int");
        if (x1 < 1 || x1 > N || y1 < 1 || y1 > N)
            throw new ArgumentOutOfRangeException("Start point must be in the range from (1, 1) to (N, N)");

        if (!int.TryParse(inputs[3], out var x2))
            throw new ArgumentException("Can't convert x2 to int");
        if (!int.TryParse(inputs[4], out var y2))
            throw new ArgumentException("Can't convert y2 to int");
        if (x2 < 1 || x2 > N || y2 < 1 || y2 > N)
            throw new ArgumentOutOfRangeException("End point must be in the range from (1, 1) to (N, N)");

        return (N, new Point(x1 - 1, y1 - 1), new Point(x2 - 1, y2 - 1));
    }

    public static int GetResult(uint N, Point start, Point end)
    {
        var board = new bool[N, N];

        var queue = new Queue<Node>();
        queue.Enqueue(new Node(start.X, start.Y, 0));

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node.X == end.X && node.Y == end.Y)
            {
                return node.Count;
            }

            foreach (var move in Moves)
            {
                var newX = node.X + move.X;
                var newY = node.Y + move.Y;

                if (IsInside(N, newX, newY) && !board[newY, newX])
                {
                    queue.Enqueue(new Node(newX, newY, node.Count + 1));
                    board[newY, newX] = true;
                }
            }
        }

        throw new Exception("Unable to find result");
    }

    private static bool IsInside(uint N, int x, int y)
    {
        return x >= 0 && x < N && y >= 0 && y < N;
    }

    public static void SaveOutput(int output)
    {
        using (var sw = new StreamWriter("OUTPUT.txt"))
        {
            sw.WriteLine(output);
        }
    }
}
public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X + 1}, {Y + 1})";
    }
}

public struct Node
{
    public int X;
    public int Y;
    public int Count;

    public Node(int x, int y, int count)
    {
        X = x;
        Y = y;
        Count = count;
    }
}
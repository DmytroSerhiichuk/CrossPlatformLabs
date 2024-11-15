namespace LabLib
{
    public class Lab_3
    {
        static readonly Point[] Moves = new Point[8]
        {
            new Point(-2, -1), new Point(-1, -2),
            new Point(1, -2), new Point(2, -1),
            new Point(2, 1), new Point(1, 2),
            new Point(-1, 2), new Point(-2, 1)
        };

        public uint N { get; private set; }
        public Point Start { get; private set; }
        public Point End { get; private set; }

        public Lab_3(string inputPath)
        {
            var inputs = ReadInput(inputPath);

            (N, Start, End) = InitInputs(inputs);

            Console.WriteLine($"N: {N}");
            Console.WriteLine($"Start: {Start}");
            Console.WriteLine($"End: {End}\n");
        }
        public Lab_3(string n, string x1, string y1, string x2, string y2)
        {
            (N, Start, End) = InitInputs(new string[] { n, x1, y1, x2, y2 });

			Console.WriteLine($"N: {N}");
			Console.WriteLine($"Start: {Start}");
			Console.WriteLine($"End: {End}\n");
		}

        public int Do()
        {
            var res = GetResult(N, Start, End);

            Console.WriteLine($"Result: {res}");

            return res;
        }

        string[] ReadInput(string file)
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

        (uint, Point, Point) InitInputs(string[] inputs)
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

        int GetResult(uint N, Point start, Point end)
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

            throw new InvalidOperationException("Unable to find result");
        }

        bool IsInside(uint N, int x, int y)
        {
            return x >= 0 && x < N && y >= 0 && y < N;
        }

        public void SaveOutput(int output, string path)
        {
            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(output);
            }
        }
    }
}

public struct Point
{
    public int X { get; private set; }
    public int Y { get; private set; }

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
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Count { get; private set; }

    public Node(int x, int y, int count)
    {
        X = x;
        Y = y;
        Count = count;
    }
}

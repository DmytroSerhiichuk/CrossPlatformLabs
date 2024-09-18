namespace Lab_1;

class Program
{
    private const string ValidChars = "0123456789abcdef?";

    static void Main(string[] args)
    {
        (var p1, var p2) = ReadInput();

        Console.WriteLine(p1);
        Console.WriteLine(p2);
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
}
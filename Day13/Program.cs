using System.Text.RegularExpressions;
using Common;

string[] inputs = null;
await FileReadHelper.ReadFileAsync(content =>
    inputs = content.Split($"{Environment.NewLine}{Environment.NewLine}"));

Regex regex = new Regex(@"(?<=X[+=])\d+|(?<=Y[+=])\d+");
Console.WriteLine(CountNumberPress(inputs));
Console.WriteLine(CountNumberPress(inputs, 10_000_000_000_000));

return;

long CountNumberPress(string[] inputs, long offset = 0)
{
    long result = 0;
    foreach (var input in inputs)
    {
        var numbers = regex.Matches(input).ToArray();
        double x_a = double.Parse(numbers[0].ToString());
        double y_a = double.Parse(numbers[1].ToString());
        double x_b = double.Parse(numbers[2].ToString());
        double y_b = double.Parse(numbers[3].ToString());
        double x = double.Parse(numbers[4].ToString()) + offset;
        double y = double.Parse(numbers[5].ToString()) + offset;

        var a = ((x * y_b) - (y * x_b)) / ((x_a * y_b) - (y_a * x_b));
        if (a - (long)a == 0)
        {
            double b = (x - (x_a * a)) / x_b;
            result += Calculate((long)a, (long)b);
        }
    }
    return result;
}

long Calculate(long a, long b) => 3 * a + b;

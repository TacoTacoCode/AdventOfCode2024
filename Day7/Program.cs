using Common;

long result = 0;
await FileReadHelper.ReadLineAsync(line =>
{
    var split = line.Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var total = long.Parse(split[0]);
    var numbers = split[1].Split(' ').Select(long.Parse).ToArray();

    if (TryConcatAdditionMutiply(total, numbers))
    {
        result += total;
    }
});

Console.WriteLine(result);

return;

bool TryConcatAdditionMutiply(long total, long[] numbers)
{
    if (numbers.Length == 1)
    {
        return total == numbers[0];
    }

    return TryConcatAdditionMutiply(total, [ConcatTwoNumber(numbers[0], numbers[1]), .. numbers[2..]])
        || TryConcatAdditionMutiply(total, [(numbers[0] + numbers[1]), .. numbers[2..]])
        || TryConcatAdditionMutiply(total, [(numbers[0] * numbers[1]), .. numbers[2..]]);
}

long ConcatTwoNumber(long num1, long num2)
{
    if (num2 == 0)
    {
        return num1;
    }

    var digitNum2 = (int)Math.Floor(Math.Log10(Math.Abs(num2))) + 1;
    return (num1 * (long)Math.Pow(10, digitNum2)) + num2;
}

using System.Runtime.InteropServices;
using Common;

List<long> inputNumbers = null;
Dictionary<long, List<long>> _cache = new Dictionary<long, List<long>>()
{
    {0 , [ 1 ] }
};
await FileReadHelper.ReadLineAsync(Initialize);
var distinctInput = inputNumbers.Distinct();
var countInput = inputNumbers.CountBy(x => x).ToDictionary(x => x.Key, x => (long)x.Value);
Console.WriteLine(Blink2(distinctInput, countInput, 25));
Console.WriteLine(Blink2(distinctInput, countInput, 75));
//Console.WriteLine(Blink2(distinctInput, countInput, 100));
return;

void Initialize(string line)
{
    var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse);
    inputNumbers = [.. split];
}

long Blink2(IEnumerable<long> inputs, Dictionary<long, long> count, int noBlink)
{
    if (noBlink == 0)
    {
        return count.Values.Sum();
    }

    HashSet<long> generate = [];
    Dictionary<long, long> dupCount = [];

    foreach (var input in inputs)
    {
        if (_cache.TryGetValue(input, out var generated))
        {
            generated.ForEach(g =>
            {
                generate.Add(g);
                TryAddOrIncrease(dupCount, g, count[input]);
            });
            continue;
        }

        string inputStr = input.ToString();
        if (inputStr.Length % 2 == 0)
        {
            int middle = inputStr.Length / 2;
            long firstPart = long.Parse(inputStr[..middle]);
            long secondPart = long.Parse(inputStr[middle..]);

            if (firstPart == secondPart)
            {
                generate.Add(firstPart);
                TryAddOrIncrease(dupCount, firstPart, 2 * count[input]);
            }
            else
            {
                generate.Add(firstPart);
                TryAddOrIncrease(dupCount, firstPart, count[input]);
                generate.Add(secondPart);
                TryAddOrIncrease(dupCount, secondPart, count[input]);
            }

            _cache.Add(input, [firstPart, secondPart]);

            continue;
        }

        long gen = input * 2024;

        generate.Add(gen);
        TryAddOrIncrease(dupCount, gen, count[input]);
        _cache.Add(input, [gen]);

        continue;
    }

    return Blink2(generate, dupCount, noBlink - 1);
}

void TryAddOrIncrease(Dictionary<long, long> dupCount, long key, long value)
{
    if (!dupCount.TryAdd(key, value))
    {
        dupCount[key] += value;
    }
}

List<long> Blink(List<long> inputs, int noBlink)
{
    List<long> generate = [];
    foreach (var input in inputs)
    {
        if (_cache.TryGetValue(input, out var generated))
        {
            generate.AddRange(generated);

            continue;
        }

        string inputStr = input.ToString();
        if (inputStr.Length % 2 == 0)
        {
            int middle = inputStr.Length / 2;
            long firstPart = long.Parse(inputStr[..middle]);
            long secondPart = long.Parse(inputStr[middle..]);

            generate.Add(firstPart);
            generate.Add(secondPart);
            _cache.Add(input, [firstPart, secondPart]);

            continue;
        }

        long gen = input * 2024;
        generate.Add(gen);
        _cache.Add(input, [gen]);
        continue;
    }

    return noBlink == 1 ? generate : Blink(generate, noBlink - 1);
}

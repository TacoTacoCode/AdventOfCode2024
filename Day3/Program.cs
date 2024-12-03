// part 1
using Common;

List<PariHolder> pairs = [];
List<PariHolder> invalid = [];
await FileReadHelper.ReadFileAsync(content =>
{
    BuildPair(content, pairs);
    BuildInvalid(content, invalid);
});

var sum1 = 0;
var sum2 = 0;
foreach (var pair in pairs)
{
    sum1 += pair.Mul;

    if (invalid.All(p => pair.Pos < p.From || p.To < pair.Pos))
    {
        sum2 += pair.Mul;
    }
}
System.Console.WriteLine(sum1);
System.Console.WriteLine(sum2);

void BuildPair(string content, List<PariHolder> pairs)
{
    var index = 0;
    while (true)
    {
        var startIndex1 = content.IndexOf("mul(", index) + 4;
        if (startIndex1 == 3)
        {
            break;
        }
        var endIndex1 = content.IndexOf(",", startIndex1);
        if (endIndex1 == -1)
        {
            break;
        }
        if (!int.TryParse(content[startIndex1..endIndex1], out var firstNum))
        {
            index = startIndex1;
            continue;
        }
        var startIndex2 = endIndex1 + 1;
        var endIndex2 = content.IndexOf(")", startIndex2);
        if (endIndex2 == -1)
        {
            break;
        }
        if (!int.TryParse(content[startIndex2..endIndex2], out var secondNum))
        {
            index = startIndex2;
            continue;
        }

        pairs.Add(new PariHolder(firstNum, secondNum, startIndex1));
        index = endIndex2 + 1;
    }
}

void BuildInvalid(string content, List<PariHolder> invalid)
{
    var index = 0;
    while (true)
    {
        var startIndex1 = content.IndexOf("don't()", index);
        if (startIndex1 == -1)
        {
            invalid.Add(new PariHolder(-1, -1));
            break;
        }
        var endIndex1 = content.IndexOf("do()", startIndex1);
        if (endIndex1 == -1)
        {
            invalid.Add(new PariHolder(startIndex1, content.Length - 1));
            break;
        }
        invalid.Add(new PariHolder(startIndex1, endIndex1));
        index = endIndex1 + 4;
    }
}

class PariHolder(int first, int second, int pos = -1)
{
    public int From { get; } = first;
    public int To { get; } = second;
    public int Pos { get; } = pos;
    public int Mul => From * To;
}
// part 1
using Common;


var count = 0;
var count2 = 0;
await FileReadHelper.ReadLineAsync(line =>
{
    if (IsSafe(line))
    {
        count++;
    }
    if (IsSafe(line, true))
    {
        count2++;
    }
});

System.Console.WriteLine(count);
System.Console.WriteLine(count2);

bool IsSafe(string line, bool ignoreOneFailed = false)
{
    var split = line.Split(separator: ' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToList();

    var indexToCheck = GetFalsyIndex(split);
    if (!ignoreOneFailed)
    {
        return indexToCheck == -1;
    }

    if (indexToCheck == -1)
    {
        return true;
    }

    var modifiedSplit = new List<int>(split);

    for (int i = indexToCheck; i >= 0; i--)
    {
        modifiedSplit.RemoveAt(i);
        if (GetFalsyIndex(modifiedSplit) == -1)
        {
            return true;
        }
        modifiedSplit = [.. split];
    }

    return false;
}

int GetFalsyIndex(List<int> split)
{
    for (int i = 1; i < split.Count; i++)
    {
        var prevIndex = i - 1;
        if (split[i] == split[index: prevIndex])
        {
            return i;
        }

        if (Math.Abs(split[i] - split[prevIndex]) > 3)
        {
            return i;
        }

        if (prevIndex == 0)
        {
            continue;
        }

        if ((split[i] > split[prevIndex] && split[prevIndex] < split[prevIndex - 1]) ||
            (split[i] < split[prevIndex] && split[prevIndex] > split[prevIndex - 1]))
        {
            return i;
        }
    }

    return -1;
}
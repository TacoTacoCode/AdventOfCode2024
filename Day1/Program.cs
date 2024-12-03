using Common;

// part 1
var list1 = new List<int>();
var list2 = new List<int>();
string[] split;

await FileReadHelper.ReadLineAsync(line =>
{
    split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    list1.Add(int.Parse(split[0]));
    list2.Add(int.Parse(split[1]));
});

list1.Sort();
list2.Sort();

var sum1 = 0;
var similarity = new Dictionary<int, int>(list2.Count);
var count = 1;
for (int i = 1; i < list2.Count; i++)
{
    sum1 += Math.Abs(list1[i] - list2[i]);

    if (list2[i] != list2[i - 1])
    {
        similarity.Add(list2[i - 1], count);
        count = 1;
    }
    else
    {
        count++;
    }
}
Console.WriteLine(sum1);

// part2
var sum2 = list1.Aggregate(0, (prev, cur) =>
{
    if (similarity.TryGetValue(cur, out var val))
    {
        return prev + cur * val;
    }
    else
    {
        return prev;
    }
});

Console.WriteLine(sum2);
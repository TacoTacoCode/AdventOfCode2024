using System.Numerics;
using Common;
using Day10;

var direction = new Dictionary<Direction, Complex>
{
    { Direction.Up, new Complex(-1, 0) },
    { Direction.Down, new Complex(1, 0) },
    { Direction.Left, new Complex(0, -1) },
    { Direction.Right, new Complex(0, 1) },
};

Dictionary<Complex, int> grid = [];
await FileReadHelper.ReadLineAsync(Initialize);

Dictionary<Complex, List<Complex>> paths = [];
foreach (var (cell, value) in grid)
{
    if (value == 0)
    {
        paths.Add(cell, []);
        FindAllPathTo9(paths, cell, cell);
    }
}

int result = 0;
int result2 = 0;
foreach (var (_, listDestination) in paths)
{
    result += listDestination.ToHashSet().Count;
    result2 += listDestination.Count;
}
Console.WriteLine(result);
Console.WriteLine(result2);

return;

void FindAllPathTo9(Dictionary<Complex, List<Complex>> result, Complex source, Complex start, Complex? offset = null)
{
    var nextCell = start;
    if (offset.HasValue)
    {
        nextCell += offset.Value;
        if (!grid.TryGetValue(nextCell, out var nextNumber))
        {
            return;
        }

        if (nextNumber - grid[start] != 1)
        {
            return;
        }

        if (nextNumber == 9)
        {
            result[source].Add(nextCell);
            return;
        }
    }

    FindAllPathTo9(result, source, nextCell, direction[Direction.Up]);
    FindAllPathTo9(result, source, nextCell, direction[Direction.Right]);
    FindAllPathTo9(result, source, nextCell, direction[Direction.Left]);
    FindAllPathTo9(result, source, nextCell, direction[Direction.Down]);
}

void Initialize(string line, int index)
{
    for (int i = 0; i < line.Length; i++)
    {
        grid.Add(new Complex(index, i), int.Parse(line[i].ToString()));
    }
}
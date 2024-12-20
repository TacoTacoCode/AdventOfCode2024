using System.Numerics;
using Common;
using Day12;

Dictionary<Direction, Complex> fourDirections = new Dictionary<Direction, Complex>
{
    { Direction.Up, new Complex(-1, 0) },
    { Direction.Down, new Complex(1, 0) },
    { Direction.Left, new Complex(0, -1) },
    { Direction.Right, new Complex(0, 1) },
};

Dictionary<Complex, char> grid = [];
int squareSize = await FileReadHelper.ReadLineAsync(Initialize);

List<Block> blocks = BuildBlock(fourDirections, grid);

Console.WriteLine(blocks.Aggregate(0, (curr, next) => curr + GetBlockRegion(next)));
Console.WriteLine(blocks.Aggregate(0, (curr, next) => curr + GetBlockRegion2(next)));
return;

void Initialize(string line, int index)
{
    for (int i = 0; i < line.Length; i++)
    {
        grid.Add(new Complex(index, i), line[i]);
    }
}

List<Block> BuildBlock(Dictionary<Direction, Complex> fourDirections, Dictionary<Complex, char> grid)
{
    List<Block> blocks = [];
    HashSet<Complex> processedPosition = [];
    Stack<Complex> processStack = [];

    while (processedPosition.Count < grid.Count)
    {
        var (currentPos, currentVal) = grid.First(x => !processedPosition.Contains(x.Key));
        var block = new Block(currentVal);
        processStack.Push(currentPos);

        while (processStack.TryPop(out var tmp))
        {
            Element curEl = new Element(tmp);

            foreach (var (direction, offset) in fourDirections)
            {
                Complex nextPos = tmp + offset;
                if (grid.TryGetValue(nextPos, out var nextVal))
                {
                    if (nextVal != currentVal)
                    {
                        continue;
                    }
                    curEl.CoveredDirections |= direction;

                    if (processedPosition.Contains(nextPos))
                    {
                        continue;
                    }
                    processStack.Push(nextPos);
                }
            }

            processedPosition.Add(tmp);
            block.AddItem(curEl);
        }

        blocks.Add(block);
    }

    return blocks;
}

int GetBlockRegion(Block block)
{
    return block.Count * (block.HasTops.Count()
                            + block.HasRights.Count()
                            + block.HasBottoms.Count()
                            + block.HasLefts.Count());
}

int GetBlockRegion2(Block block)
{
    int countSide = CountNonConsecutive(block.HasTops.GroupBy(e => (int)e.Position.Real, e => (int)e.Position.Imaginary)) +
                    CountNonConsecutive(block.HasBottoms.GroupBy(e => (int)e.Position.Real, e => (int)e.Position.Imaginary)) +
                    CountNonConsecutive(block.HasRights.GroupBy(e => (int)e.Position.Imaginary, e => (int)e.Position.Real)) +
                    CountNonConsecutive(block.HasLefts.GroupBy(e => (int)e.Position.Imaginary, e => (int)e.Position.Real));

    return block.Count * countSide;
}

int CountNonConsecutive(IEnumerable<IGrouping<int, int>> groups)
{
    int count = 0;

    foreach (var group in groups)
    {
        count++;
        group.Order().Aggregate(group.First(), (curr, next) =>
        {
            if (next - curr > 1)
            {
                count++;
            }
            return next;
        });
    }

    return count;
}
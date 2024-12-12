using System.Diagnostics;
using System.Numerics;
using Common;
using Day6;

var guardState = new Dictionary<char, Direction>
{
    { '^', Direction.Up },
    { 'v', Direction.Down },
    { '<', Direction.Left },
    { '>', Direction.Right },
};

var directionStep = new Dictionary<Direction, Complex>
{
    { Direction.Up, new Complex(-1, 0) },
    { Direction.Down, new Complex(1, 0) },
    { Direction.Left, new Complex(0, -1) },
    { Direction.Right, new Complex(0, 1) },
};

Dictionary<Complex, CellData> grid = [];
Complex? initPosition = null;
Direction initDirection = Direction.None;
await FileReadHelper.ReadLineAsync(Initialize);

Console.WriteLine(Part1());

grid.Clear();
initPosition = null;
initDirection = Direction.None;
await FileReadHelper.ReadLineAsync(Initialize);

Console.WriteLine(Part2());
return;

int Part1()
{
    var currentPosition = initPosition.Value;
    HashSet<Complex> walkedCell = [currentPosition];
    var currentDirection = initDirection;
    while (true)
    {
        var (nextPosition, nextDirection) = GetNextPosition(currentPosition, currentDirection, grid);
        if (!nextPosition.HasValue)
        {
            return walkedCell.Count;
        }

        grid[nextPosition.Value].Direction |= nextDirection;
        currentPosition = nextPosition.Value;
        currentDirection = nextDirection;

        walkedCell.Add(currentPosition);
    }
}

int Part2()
{
    HashSet<Complex> placedObject = [];
    HashSet<Complex> notPlacedObject = [];

    var currentPosition = initPosition.Value;
    var currentDirection = initDirection;

    var tmpGrid = grid.ToDictionary(x => x.Key, x => CellData.Clone(x.Value));
    while (true)
    {
        var (futurePosition, futureDirection) = GetNextPosition(currentPosition, currentDirection, grid);
        if (!futurePosition.HasValue)
        {
            return placedObject.Count;
        }

        if (notPlacedObject.TryGetValue(futurePosition.Value, out _))
        {
            grid[futurePosition.Value].Direction |= futureDirection;
            tmpGrid[futurePosition.Value].Direction |= futureDirection;
            currentPosition = futurePosition.Value;
            currentDirection = futureDirection;
            continue;
        }

        tmpGrid[futurePosition.Value].CellValue = '#';

        while (true)
        {
            var (nextPosition, nextDirection) = GetNextPosition(currentPosition, currentDirection, tmpGrid);
            // Current obstacle cannot stop the guard
            if (!nextPosition.HasValue)
            {
                notPlacedObject.Add(futurePosition.Value);
                grid[futurePosition.Value].Direction |= futureDirection;
                currentPosition = futurePosition.Value;
                currentDirection = futureDirection;

                break;
            }

            // Already been in this cell with same direction
            if ((tmpGrid[nextPosition.Value].Direction & nextDirection) != Direction.None)
            {
                placedObject.Add(futurePosition.Value);
                grid[futurePosition.Value].Direction |= futureDirection;
                currentPosition = futurePosition.Value;
                currentDirection = futureDirection;

                break;
            }
            tmpGrid[nextPosition.Value].Direction |= nextDirection;
            currentPosition = nextPosition.Value;
            currentDirection = nextDirection;
        }

        // reset tmpGrid to be same as grid
        tmpGrid[futurePosition.Value].CellValue = '.';
        foreach (var pair in tmpGrid)
        {
            pair.Value.Direction = grid[pair.Key].Direction;
        }
    }
}

(Complex?, Direction) GetNextPosition(Complex sourcePosition, Direction sourceDirection, Dictionary<Complex, CellData> data)
{
    var step = directionStep[sourceDirection];
    var nextPosition = sourcePosition + step;

    if (!data.TryGetValue(nextPosition, out var nextCellData))
    {
        return (null, sourceDirection);
    }

    var currentDirection = sourceDirection;

    while (nextCellData.CellValue == '#')
    {
        currentDirection = GetRightDirectionOfCurrent(currentDirection);
        step = directionStep[currentDirection];
        nextPosition = sourcePosition + step;

        if (!data.TryGetValue(nextPosition, out nextCellData))
        {
            return (null, currentDirection);
        }
    }

    return (nextPosition, currentDirection);
}

void Initialize(string line, int index)
{
    for (int i = 0; i < line.Length; i++)
    {
        var position = new Complex(index, i);

        if (initPosition is null && guardState.TryGetValue(line[i], out var direction))
        {
            initPosition = position;
            initDirection = direction;
            grid.Add(key: position, new CellData(line[i], direction));
        }
        else
        {
            grid.Add(key: position, new CellData(line[i]));
        }
    }
}

Direction GetRightDirectionOfCurrent(Direction currDirection)
{
    int nextIntValue = (int)currDirection << 1;

    if (Enum.IsDefined(typeof(Direction), nextIntValue))
    {
        return (Direction)nextIntValue;
    }

    return Direction.Up;
}
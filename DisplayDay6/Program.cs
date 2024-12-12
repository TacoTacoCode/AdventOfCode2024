using Common;
using Day6;

char[,] matrix = null;
int squareSize = -1;
int curGuardCol = -1;
int currGuardRow = -1;
Direction currGuardDirection = Direction.Up;
var guardState = new Dictionary<char, Direction>
{
    { '^', Direction.Up }, { 'v', Direction.Down },
    { '<', Direction.Left }, { '>', Direction.Right },
};
var guardStateReverse = new Dictionary<Direction, char>
{
    { Direction.Up, '^' },
    { Direction.Down, 'v' },
    { Direction.Left, '<' },
    { Direction.Right, '>' },
};

await FileReadHelper.ReadLineAsync((line, index) =>
{
    ParseData(line, index);
});

var result = 1;
while (true)
{
    //System.Console.WriteLine($"{currGuardRow}-{curGuardCol}-{currGuardDirection}");
    var (nextGuardRow, nextGuardCol) = GetNextGuardPos(currGuardDirection, currGuardRow, curGuardCol);
    if (nextGuardRow == squareSize ||
        nextGuardCol == squareSize ||
        nextGuardRow == -1 ||
        nextGuardCol == -1)
    {
        break;
    }

    matrix[currGuardRow, curGuardCol] = 'X';

    while (matrix[nextGuardRow, nextGuardCol] == '#')
    {
        currGuardDirection = GetRightDirectionOfCurrent(currGuardDirection);
        (nextGuardRow, nextGuardCol) = GetNextGuardPos(currGuardDirection, currGuardRow, curGuardCol);
    }

    (currGuardRow, curGuardCol) = (nextGuardRow, nextGuardCol); //step
    if (matrix[currGuardRow, curGuardCol] != 'X')
    {
        result++;
    }

    matrix[currGuardRow, curGuardCol] = guardStateReverse[currGuardDirection]; // update map
    PrintMap();
}
Console.WriteLine(result);
return;

void ParseData(string line, int index)
{
    if (index == 0)
    {
        squareSize = line.Length;
        matrix = new char[squareSize, squareSize];
    }

    for (var i = 0; i < squareSize; i++)
    {
        var currentChar = line[i];
        if (guardState.TryGetValue(currentChar, out currGuardDirection))
        {
            currGuardRow = index;
            curGuardCol = i;
        }

        matrix[index, i] = currentChar;
    }
}

void PrintMap()
{
    Console.Clear();
    for (var row = 0; row < squareSize; row++)
    {
        for (var col = 0; col < squareSize; col++)
        {
            Console.Write(matrix[row, col]);
        }

        Console.WriteLine();
    }

    Console.WriteLine("======================");
}

(int nextRow, int nextCol) GetNextGuardPos(Direction currDirection, int currRow, int currCol)
{
    return currDirection switch
    {
        Direction.Up => (currRow - 1, currCol),
        Direction.Down => (currRow + 1, currCol),
        Direction.Left => (currRow, currCol - 1),
        Direction.Right => (currRow, currCol + 1),
        _ => throw new ArgumentOutOfRangeException()
    };
}

Direction GetRightDirectionOfCurrent(Direction currDirection)
{
    return Enum.IsDefined(currDirection + 1) ? currDirection + 1 : Direction.Up;
}
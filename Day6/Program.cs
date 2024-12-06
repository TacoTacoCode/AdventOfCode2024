// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
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
    { Direction.Up, '^'  },
    { Direction.Down, 'v'  },
    { Direction.Left, '<'  },
    { Direction.Right, '>'  },
};

await FileReadHelper.ReadLineAsync((line, index) =>
{
    ParseData(line, index);
});
//PrintMap();
var result = 1;
var result2 = 0;
while (true)
{
    var (nextGuardRow, nextGuardCol) = GetNextGuardPos(currGuardDirection, currGuardRow, curGuardCol);
    if (nextGuardRow == squareSize ||
        nextGuardCol == squareSize||
        nextGuardRow == -1 ||
        nextGuardCol == -1)
    {
        break;
    }

    var rightDirectionOfCurr = GetRightDirectionOfCurrent(currGuardDirection);
    var (rightRow1, rightCol1) = GetNextGuardPos(rightDirectionOfCurr, currGuardRow, curGuardCol);
    var (rightRow2, rightCol2) = GetNextGuardPos(rightDirectionOfCurr, rightRow1, rightCol1);
    var (rightRow3, rightCol3) = GetNextGuardPos(rightDirectionOfCurr, rightRow2, rightCol2);
    if (matrix[nextGuardRow, nextGuardCol] ==  '.' && // can continue
        matrix[rightRow2, rightCol2] != '.' && // your second right already go
        (matrix[rightRow1, rightCol1] != '.' ||  matrix[rightRow3, rightCol3] != '.')) // can turn right and repeat
    {
        Console.WriteLine("Can put obstacle");
        result2++;
    }

    matrix[currGuardRow, curGuardCol] = 'X';

    if (matrix[nextGuardRow, nextGuardCol] == '#')
    {
        currGuardDirection = GetRightDirectionOfCurrent(currGuardDirection);
        (nextGuardRow, nextGuardCol) = GetNextGuardPos(currGuardDirection, currGuardRow, curGuardCol);
    }

    (currGuardRow, curGuardCol) = (nextGuardRow, nextGuardCol); //step
    if (matrix[currGuardRow, curGuardCol] !=  'X')
    {
        result++;
    }
    matrix[currGuardRow, curGuardCol] = guardStateReverse[currGuardDirection]; // update map
    PrintMap();
}
//PrintMap();
Console.WriteLine(result);
Console.WriteLine(result2);
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
    //Console.Clear();
    for (var row = 0; row < squareSize; row++)
    {
        for (var col = 0; col < squareSize; col++)
        {
            Console.Write(matrix[row, col]);
        }

        Console.WriteLine();
    }

    Console.WriteLine("======================");
    //Thread.Sleep(300);
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
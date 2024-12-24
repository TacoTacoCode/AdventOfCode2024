using System.Numerics;
using System.Runtime.CompilerServices;
using Common;
using Day15;

var directionOffset = new Dictionary<char, Complex>
{
    { '^', new Complex(-1, 0) },
    { 'v', new Complex(1, 0) },
    { '<', new Complex(0, -1) },
    { '>', new Complex(0, 1) },
};

Dictionary<Complex, char> grid = [];
int squareSize = 0;
string command = string.Empty;
Complex robotPos = default;

Dictionary<Complex, char> grid2 = [];
List<Box> listbox = [];
Complex robotPos2 = default;

await FileReadHelper.ReadFileAsync(Initialize);
// PrintMap();
// PrintMap2();
Console.WriteLine(Part1());
Console.WriteLine(Part2());

return;

double Part1()
{
    foreach (var direction in command)
    {
        Complex currOffset = directionOffset[direction];
        Complex nextPos = robotPos + currOffset;
        if (grid[nextPos] == '.')
        {
            grid[robotPos] = '.';
            grid[nextPos] = '@';
            robotPos = nextPos;

            continue;
        }

        if (grid[nextPos] == '#')
        {
            continue;
        }

        Complex tmp = nextPos + currOffset;
        while (grid[tmp] == 'O')
        {
            tmp += currOffset;
        }

        if (grid[tmp] == '.')
        {
            grid[robotPos] = '.';
            grid[nextPos] = '@';
            grid[tmp] = 'O';
            robotPos = nextPos;
        }

    }
    return grid
                .Where(p => p.Value == 'O')
                .Select(p => p.Key)
                .Aggregate(0d, (curr, next) => curr + (100 * next.Real + next.Imaginary));
}

double Part2()
{
    foreach (var direction in command)
    {
        Complex currOffset = directionOffset[direction];
        Complex nextPos = robotPos2 + currOffset;

        if (grid2[nextPos] == '.')
        {
            grid2[robotPos2] = '.';
            grid2[nextPos] = '@';
            robotPos2 = nextPos;
            continue;
        }

        if (grid2[nextPos] == '#')
        {
            continue;
        }

        Box box = listbox.First(x => x.IsBox(nextPos));
        HashSet<Box> relatedBoxes = [box];
        GetRelatedBoxed(box, currOffset, relatedBoxes);

        Box[] relatedBoxArr = relatedBoxes.ToArray();
        // in GetRelatedBoxed, we stop when last box not next to a box
        // so reverse would make loop faster here
        bool canMove = true;
        for (int i = relatedBoxArr.Length - 1; i >= 0; i--)
        {
            Box rb = relatedBoxArr[i];
            if (grid2[rb.Left + currOffset] == '#' || grid2[rb.Right + currOffset] == '#')
            {
                canMove = false;
                break;
            }
        }

        // If the box can move, we move, then set the previous pos as '.'
        HashSet<Complex> oldPos = [];
        if (canMove)
        {
            foreach (var rBox in relatedBoxes)
            {
                oldPos.Add(rBox.Left);
                oldPos.Add(rBox.Right);

                rBox.Moved(currOffset);
                grid2[rBox.Left] = '[';
                grid2[rBox.Right] = ']';
            }

            foreach (Complex oPos in oldPos)
            {
                if (listbox.Any(b => b.IsBox(oPos)))
                {
                    continue;
                }
                grid2[oPos] = '.';
            }
            grid2[robotPos2] = '.';
            grid2[nextPos] = '@';
            robotPos2 = nextPos;
        }
    }
    return listbox
        .Select(b => b.Left)
        .Aggregate(0d, (curr, next) => curr + (100 * next.Real + next.Imaginary));
}

void GetRelatedBoxed(Box box, Complex direction, HashSet<Box> result)
{
    Box leftRelated = listbox.FirstOrDefault(x => x.IsBox(box.Left + direction));
    Box rightRelated = listbox.FirstOrDefault(x => x.IsBox(box.Right + direction));

    if (leftRelated != null && leftRelated != box)
    {
        result.Add(leftRelated);
        GetRelatedBoxed(leftRelated, direction, result);

    }

    if (rightRelated != null && rightRelated != box)
    {
        result.Add(rightRelated);
        GetRelatedBoxed(rightRelated, direction, result);
    }
}

void Initialize(string content)
{
    string[] split = content.Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);

    string[] gridContent = split[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    squareSize = gridContent.Length;

    for (int i = 0; i < squareSize; i++)
    {
        string line = gridContent[i];
        for (int j = 0, k = 0; j < line.Length; j++, k = j * 2)
        {
            Complex tmp = new Complex(i, j);
            char value = line[j];
            grid.Add(tmp, value);

            Complex left = new Complex(i, k);
            Complex right = new Complex(i, k + 1);
            switch (value)
            {
                case 'O':
                    grid2.Add(left, '[');
                    grid2.Add(right, ']');

                    listbox.Add(new Box(left, right));
                    break;
                case '@':
                    grid2.Add(left, '@');
                    grid2.Add(right, '.');

                    robotPos = tmp;
                    robotPos2 = left;
                    break;
                default:
                    grid2.Add(left, value);
                    grid2.Add(right, value);
                    break;

            }
        }
    }
    command = split[1].Replace(Environment.NewLine, string.Empty);

    // PrintMap();
    Console.WriteLine(command);
    Console.WriteLine(robotPos);
}

void PrintMap()
{
    for (var row = 0; row < squareSize; row++)
    {
        for (var col = 0; col < squareSize; col++)
        {
            Console.Write(grid[new Complex(row, col)]);
        }

        Console.WriteLine();
    }

    Console.WriteLine("======================");
}

void PrintMap2()
{
    for (var row = 0; row < squareSize; row++)
    {
        for (var col = 0; col < squareSize * 2; col++)
        {
            Console.Write(grid2[new Complex(row, col)]);
        }

        Console.WriteLine();
    }

    Console.WriteLine("======================");
}


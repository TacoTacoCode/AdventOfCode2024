using System.Numerics;
using System.Text.RegularExpressions;
using Common;

List<(Complex Position, Complex Speed)> robotInfo = [];
int wide = 101;
int tall = 103;

await FileReadHelper.ReadLineAsync(Initialize);
Console.WriteLine(Part1());
Console.WriteLine(Part2());

return;

void Initialize(string line)
{
    Regex numberRegex = new Regex(@"-?\d+");
    var matches = numberRegex.Matches(line).Select(x => double.Parse(x.ToString())).ToArray();
    var robotPosition = new Complex(matches[0], matches[1]);
    var robotSpeed = new Complex(matches[2], matches[3]);

    robotInfo.Add((robotPosition, robotSpeed));
}

int Part1()
{
    int middleCol = wide / 2;
    int middleRow = tall / 2;
    int topLeft = 0;
    int topRight = 0;
    int bottomLeft = 0;
    int bottomRight = 0;

    GetPositionAfter(100, robotInfo, (newCol, newRow, _) =>
    {
        if (newCol < middleCol && newRow < middleRow)
        {
            topLeft++;
        }
        else if (newCol > middleCol && newRow < middleRow)
        {
            topRight++;
        }
        else if (newCol < middleCol && newRow > middleRow)
        {
            bottomLeft++;
        }
        else if (newCol > middleCol && newRow > middleRow)
        {
            bottomRight++;
        }
    });

    return topLeft * topRight * bottomLeft * bottomRight;
}

int Part2()
{
    List<(Complex Position, Complex Speed)> currnetInfo = [.. robotInfo];

    for (int i = 1; i <= wide * tall; i++) // wide and tall are prime, so after this, robots comeback to their first position
    {
        GetPositionAfter(i, robotInfo, (newCol, newRow, index) =>
        {
            currnetInfo.Add((new Complex(newCol, newRow), robotInfo[index].Speed));
        });
        HashSet<Complex> robotPos = currnetInfo.Select(x => x.Position).ToHashSet();

        // If there is a long line of robot next to eachother, which will longer than x% of the tall
        // We assume it the tree line
        for (float rate = 0.8f; rate >= 0.1f; rate -= 0.1f)
        {
            if (ExistTreeLine(robotPos, rate))
            {
                Print(robotPos);
                Console.WriteLine($"Second {i} at rate {rate}");
                return i;
            }
        }

        currnetInfo.Clear();
    }

    return -1; // Please check manually
}

void GetPositionAfter(int second, List<(Complex Position, Complex Speed)> currInfo, Action<double, double, int> actionOnPosition)
{
    for (int i = 0; i < currInfo.Count; i++)
    {
        var (position, speed) = currInfo[i];
        Complex speedAfter = speed * second;
        Complex positionAfter = position + speedAfter;

        var newCol = positionAfter.Real % wide;
        newCol = newCol < 0 ? wide + newCol : newCol;
        var newRow = positionAfter.Imaginary % tall;
        newRow = newRow < 0 ? tall + newRow : newRow;

        actionOnPosition(newCol, newRow, i);
    }
}

bool ExistTreeLine(HashSet<Complex> robotPos, float rate)
{
    double minimumTall = tall * rate;
    var colGroup = robotPos.GroupBy(x => x.Real)
        .OrderBy(g => g.Key)
        .Select(g => g.OrderBy(c => c.Imaginary));

    foreach (var group in colGroup)
    {
        double tmp = -2;
        List<Complex> line = [];
        foreach (var item in group)
        {
            if (item.Imaginary - tmp == 1)
            {
                line.Add(item);
                tmp = item.Imaginary;
            }
            else if (line.Count < minimumTall)
            {
                line.Clear();
                line.Add(item);
                tmp = item.Imaginary;
            }
            else
            {
                return true;
            }

        }
    }
    return false;
}

void Print(HashSet<Complex> robotPos)
{
    for (int i = 0; i < tall; i++)
    {
        for (int j = 0; j < wide; j++)
        {
            if (robotPos.Contains(new Complex(j, i)))
            {
                Console.Write("X");
            }
            else
            {
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
}
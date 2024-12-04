using System.Text;
using System.Text.RegularExpressions;
using Common;

var xmasRegex = new Regex("(?=(XMAS|SAMX))");
var lenSearch = 4; // len of XMAS to optimize search

int intputSize = -1;
List<string> horizontal = null;
List<StringBuilder> verticalSb = null;
List<string> diagonal1 = null;
List<string> diagonal2 = null;

char[,] matrix = null;

await FileReadHelper.ReadLineAsync((line, index) =>
{
    if (index == 0)
    {
        Initialize();
    }
    BuildHorizontalLine(line);
    BuildVerticalLine(line);

    BuildMatrix(line, index);
});

BuildFirstDiagonal(); //    Diagonal \
BuildSecondDiagonal(); //   Diagonal /

int countXmas = 0;
horizontal.ForEach(x => countXmas += xmasRegex.Matches(x).Count);
verticalSb.ForEach(x => countXmas += xmasRegex.Matches(x.ToString()).Count);
diagonal1.ForEach(x => countXmas += xmasRegex.Matches(x).Count);
diagonal2.ForEach(x => countXmas += xmasRegex.Matches(x).Count);

System.Console.WriteLine(countXmas);

//========================== p2
int countXmas2 = 0;
for (int row = 1; row < intputSize - 1; row++)
{
    for (int col = 1; col < intputSize - 1; col++)
    {
        if (matrix[row, col] == 'A')
        {
            var surround = new List<char>()
            {
                matrix[row + 1, col + 1],
                matrix[row - 1, col - 1],
                matrix[row + 1, col - 1],
                matrix[row - 1, col + 1]
            };
            if (surround.Any(x => x != 'M' && x != 'S'))
            {
                continue;
            }
            if (matrix[row + 1, col + 1] == matrix[row - 1, col - 1] ||
                matrix[row - 1, col + 1] == matrix[row + 1, col - 1])
            {
                continue;
            }
            countXmas2++;
        }
    }
}
System.Console.WriteLine(countXmas2);

#region method

void Initialize()
{
    intputSize = 140;
    horizontal = new List<string>(intputSize);
    verticalSb = new List<StringBuilder>(intputSize);
    diagonal1 = new List<string>(intputSize);
    diagonal2 = new List<string>(intputSize);

    matrix = new char[intputSize, intputSize];
}

void BuildHorizontalLine(string line)
{
    horizontal.Add(line);
}

void BuildVerticalLine(string line)
{
    for (int i = 0; i < intputSize; i++)
    {
        if (verticalSb.Count < intputSize)
        {
            verticalSb.Add(new StringBuilder(intputSize));
        }
        verticalSb[i].Append(line[^(i + 1)]);
    }
}

void BuildFirstDiagonal()
{
    var boundary = intputSize - lenSearch;
    var builder = new StringBuilder();
    while (boundary > 0)
    {
        var j = boundary;
        var i = 0;
        while (j < intputSize)
        {
            builder.Append(horizontal[j][i]);
            j++;
            i++;
        }
        diagonal1.Add(builder.ToString());
        builder.Clear();
        boundary--;
    }

    while (boundary <= intputSize - lenSearch)
    {
        var j = 0;
        var i = boundary;
        while (i < intputSize)
        {
            builder.Append(horizontal[j][i]);
            j++;
            i++;
        }
        diagonal1.Add(builder.ToString());
        builder.Clear();
        boundary++;
    }
}

void BuildSecondDiagonal()
{
    var boundary = lenSearch - 1;
    var builder = new StringBuilder();
    while (boundary < intputSize)
    {
        var j = 0;
        var i = boundary;
        while (i >= 0)
        {
            builder.Append(horizontal[j][i]);
            j++;
            i--;
        }
        diagonal2.Add(builder.ToString());
        builder.Clear();
        boundary++;
    }
    boundary = 1;
    while (boundary <= intputSize - lenSearch)
    {
        var j = boundary;
        var i = intputSize - 1;
        while (j < intputSize)
        {
            builder.Append(horizontal[j][i]);
            j++;
            i--;
        }
        diagonal2.Add(builder.ToString());
        builder.Clear();
        boundary++;
    }
}

void BuildMatrix(string line, int index)
{
    for (int i = 0; i < line.Length; i++)
    {
        matrix[index, i] = line[i];
    }
}

#endregion
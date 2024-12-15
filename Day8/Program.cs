using System.Numerics;
using Common;

Dictionary<char, List<Complex>> antenaLocation = [];
int size = await FileReadHelper.ReadLineAsync(Initialize);

Console.WriteLine(Part1());
Console.WriteLine(Part2());
return;

int Part2()
{
    HashSet<Complex> nodeLocation = [];
    foreach (var pair in antenaLocation)
    {
        var locations = pair.Value;
        for (int i = 0; i < locations.Count - 1; i++)
        {
            nodeLocation.Add(locations[i]);

            for (int j = i + 1; j < locations.Count; j++)
            {
                var distance = locations[i] - locations[j];
                var tmp = locations[i] + distance;
                while (InRange((int)tmp.Real, 0, size)
                    && InRange((int)tmp.Imaginary, 0, size))
                {
                    nodeLocation.Add(tmp);
                    tmp += distance;
                }

                tmp = locations[j] - distance;
                while (InRange((int)tmp.Real, 0, size) &&
                    InRange((int)tmp.Imaginary, 0, size))
                {
                    nodeLocation.Add(tmp);
                    tmp -= distance;
                }
            }
        }

        nodeLocation.Add(locations[^1]);
    }
    return nodeLocation.Count;
}

int Part1()
{
    HashSet<Complex> nodeLocation = [];
    foreach (var pair in antenaLocation)
    {
        var locations = pair.Value;
        for (int i = 0; i < locations.Count - 1; i++)
        {
            for (int j = i + 1; j < locations.Count; j++)
            {
                var distance = locations[i] - locations[j];

                // Upper node
                var tmp = locations[i] + distance;
                if (InRange((int)tmp.Real, 0, size) &&
                    InRange((int)tmp.Imaginary, 0, size))
                {
                    nodeLocation.Add(tmp);
                }

                // Lower node
                tmp = locations[j] - distance;
                if (InRange((int)tmp.Real, 0, size) &&
                    InRange((int)tmp.Imaginary, 0, size))
                {
                    nodeLocation.Add(tmp);
                }
            }
        }
    }
    return nodeLocation.Count;
}

bool InRange(int checkNum, int start, int end)
{
    return checkNum >= start && checkNum <= end;
}
void Initialize(string line, int index)
{
    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] == '.')
        {
            continue;
        }

        var location = new Complex(index, i);
        if (antenaLocation.TryGetValue(line[i], out var locations))
        {
            locations.Add(location);
        }
        else
        {
            antenaLocation.Add(line[i], [location]);
        }
    }
}
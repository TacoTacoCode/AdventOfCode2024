using Common;

var isReadingSec1 = true;
var orderRule = new Dictionary<int, List<int>>();
var totalIfNoFix = 0;
var totalIfFix = 0;
await FileReadHelper.ReadLineAsync(line =>
{
	if (string.IsNullOrEmpty(line))
	{
		isReadingSec1 = false;

		return;
	}

	if (isReadingSec1)
	{
		BuildOrderRules(line);
	}
	else
	{
		totalIfNoFix += CheckOrdering(line);
		totalIfFix += CheckOrdering(line, true);
	}
});

Console.WriteLine(totalIfNoFix);
Console.WriteLine(totalIfFix - totalIfNoFix);
return;

void BuildOrderRules(string line)
{
	var number = line.Split('|', StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
	if (orderRule.TryGetValue(number[0], out var existingList))
	{
		existingList.Add(number[1]);
	}
	else
	{
		orderRule[number[0]] = [number[1]];
	}
}

int CheckOrdering(string line, bool fixLine = false)
{
	var checkNumber = line.Split(',').Select(int.Parse).ToList();
	for (var i = 0; i < checkNumber.Count - 1; i++)
	{
		if (!orderRule.TryGetValue(checkNumber[i], out var checkList) ||
		    !checkList.Contains(checkNumber[i + 1]))
		{
			if (!fixLine)
			{
				return 0;
			}

			Swap(checkNumber, i, i + 1);
			return CheckOrdering(string.Join(',', checkNumber), true);
		}
	}

	return checkNumber[(checkNumber.Count - 1) / 2];
}

void Swap(List<int> arr, int firstIndex, int secondIndex)
{
	(arr[firstIndex], arr[secondIndex]) = (arr[secondIndex], arr[firstIndex]);
}


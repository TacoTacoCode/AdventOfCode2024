using Common;

string input = null;
await FileReadHelper.ReadLineAsync(line => input = line);

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));
return;

long Part1(string line)
{
    List<string> fileIds = FromLineToFileIds(line);

    int i = 0;
    int j = fileIds.Count - 1;
    while (i != j)
    {
        if (fileIds[i] == ".")
        {
            while (fileIds[j] == ".")
            {
                j--;
            }
            fileIds[i] = fileIds[j];
            fileIds[j] = ".";
            j--;
        }
        i++;
    }

    return CalculateCheckSum(fileIds);
}

long Part2(string line)
{
    List<string> listFileId = FromLineToFileIds(line);
    // listFileId.ForEach(Console.Write);
    // Console.WriteLine();

    int i = listFileId.Count - 1;
    string fileId = null;
    int fileLength = 0;
    int currentBlockStart = 0;
    int blockLength = 0;
    while (i > -1)
    {
        if (fileId == null)
        {
            if (listFileId[i] != ".")
            {
                fileId = listFileId[i];
                fileLength = 1;
            }

            i--;
            continue;
        }
        if (listFileId[i] == fileId)
        {
            fileLength++;
            i--;
            continue;
        }

        // We identify a file, so now move from start to find any free space
        i++;
        for (int index = currentBlockStart; index <= i; index++)
        {
            // Building the free block
            if (listFileId[index] == ".")
            {
                if (blockLength == 0)
                {
                    currentBlockStart = index;
                }
                blockLength++;
                continue;
            }
            // skip all the number
            if (blockLength == 0)
            {
                continue;
            }
            // current is not a '.', so it is the end of a free block
            // so, we do the check
            // if this block is not suitable, we reset the block and continue moving
            if (blockLength < fileLength)
            {
                blockLength = 0;
                continue;
            }
            // suitable block, move the file
            for (int j = 0; j < fileLength; j++)
            {
                listFileId[currentBlockStart + j] = fileId;
                listFileId[i + j] = ".";
            }

            blockLength = 0;
            break;
        }
        // reset every thing since the file is moved or cannot move
        // also reset the fileId so we can check next file
        currentBlockStart = 0;
        fileId = null;
        i--;
    }

    return CalculateCheckSum(listFileId);
}

List<string> FromLineToFileIds(string line)
{
    List<string> fileIds = [];
    bool isFile = true;
    int fileId = 0;
    foreach (var c in line)
    {
        var number = int.Parse(c.ToString());
        for (int index = 0; index < number; index++)
        {
            if (isFile)
            {
                fileIds.Add(fileId.ToString());
            }
            else
            {
                fileIds.Add(".");
            }
        }
        if (isFile)
        {
            fileId++;
        }
        isFile = !isFile;
    }

    return fileIds;
}

long CalculateCheckSum(List<string> fileIds)
{
    long result = 0;
    for (int index = 0; index < fileIds.Count; index++)
    {
        if (fileIds[index] != ".")
        {
            result += index * long.Parse(fileIds[index]);
        }
    }

    return result;
}
namespace Common;

public class FileReadHelper
{
    public static async Task ReadLineAsync(Action<string> actionByLine)
    {
        using (var stream = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input.txt")))
        {
            string line;
            while ((line = await stream.ReadLineAsync()) is not null)
            {
                try
                {
                    actionByLine(line);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed at: {0}", line);
                    throw;
                }
            }
        }
    }

    public static async Task ReadFileAsync(Action<string> actionOnContent)
    {
        using (var stream = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input.txt")))
        {
            var content = await stream.ReadToEndAsync();
            actionOnContent(content);
        }
    }

    public static async Task ReadLineAsync(Action<string, int> actionByLine)
    {
        using (var stream = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input.txt")))
        {
            string line;
            int index = -1;
            while ((line = await stream.ReadLineAsync()) is not null)
            {
                index++;
                try
                {
                    actionByLine(line, index);
                }
                catch (System.Exception)
                {
                    System.Console.WriteLine("Failed at: {0}", line);
                    throw;
                }
            }
        }
    }
}

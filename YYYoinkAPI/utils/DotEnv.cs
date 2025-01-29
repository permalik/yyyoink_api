namespace YYYoinkAPI.utils;

// TODO: update utils dir name
public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            var index = line.IndexOf('=');
            if (index != -1)
            {
                var key = line[..index];
                var value = line[(index + 1)..];
                Environment.SetEnvironmentVariable(key, value);
            }
            else
            {
                continue;
            }
        }
    }
}
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
                // Handle lines without an '=' if necessary
            }
        }

        // foreach (var line in File.ReadAllLines(filePath))
        // {
        //     var parts = line.Split(
        //         '=',
        //         StringSplitOptions.RemoveEmptyEntries
        //     );
        //     if (parts.Length != 2)
        //     {
        //         continue;
        //     }
        //
        //     Environment.SetEnvironmentVariable(parts[0], parts[1]);
        // }
    }
}

using Npgsql;

namespace YYYoinkAPI.Services.Postgres;

public class Database
{
    // const string connectionString = "Host=localhost;Username=tymalik;Password=wHZVp4Yzo4MtnsA4yHm4;Database=yyyoink";
    private readonly string connectionString;

    // TODO: convert into primary constructor
    public Database(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<List<string>> GetUserAsync()
    {
        var result = new List<string>();
        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        await using (var cmd = new NpgsqlCommand("SELECT * FROM accounts", conn))
        await using (var reader = await cmd.ExecutReaderAsync())
        {
            while (await reader.ReaderAsync())
            {
                result.Add(reader.GetString(0));
            }
        }

        return result;
    }
}
using Npgsql;
using YYYoinkAPI.Models;

namespace YYYoinkAPI.Services.Postgres;

public class Database
{
    private readonly string connectionString;

    // TODO: convert into primary constructor
    public Database(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<User> GetUserAsync()
    {
        await using var conn = new NpgsqlConnection(connectionString);
        await conn.OpenAsync();

        string? uuid = null;
        string? email = null;
        string? password = null;

        try
        {
            await using (var cmd = new NpgsqlCommand("SELECT * FROM accounts", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    uuid = reader.IsDBNull(4) ? null : reader.GetString(4);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    password = reader.IsDBNull(2) ? null : reader.GetString(2);
                }
            }

            if (!string.IsNullOrEmpty(uuid) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                return new User(
                    new Guid(uuid),
                    email,
                    password
                );
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"error retrieving user: {exc.Message}");
        }

        return null;
    }
}
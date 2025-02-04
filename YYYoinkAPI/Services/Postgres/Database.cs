using System.Data;
using Npgsql;
using YYYoinkAPI.Logger;
using YYYoinkAPI.Models;
using ILogger = Serilog.ILogger;

namespace YYYoinkAPI.Services.Postgres;

public class Database
{
    private readonly string _connectionString;

    // TODO: convert into primary constructor
    public Database(string _connectionString)
    {
        this._connectionString = _connectionString;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        await using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        string? uuid = null;
        string? email = null;
        string? password = null;
        string? refreshToken = null;
        try
        {
            // TODO: impl parameterized queries to prevent sql inj (https://www.npgsql.org/doc/basic-usage.html)
            string insert =
                $"INSERT INTO accounts (email, password, uuid, refresh_token) VALUES ('{user.Email}', '{user.Password}', '{user.Uuid}', '{user.RefreshToken}')";
            string select = $"SELECT * FROM accounts WHERE email = '{user.Email}'";
            await using NpgsqlBatch batch = new NpgsqlBatch(conn)
            {
                BatchCommands =
                {
                    new NpgsqlBatchCommand(insert),
                    new NpgsqlBatchCommand(select)
                }
            };
            await using (NpgsqlDataReader reader = await batch.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    uuid = reader.IsDBNull(4) ? null : reader.GetString(4);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    password = reader.IsDBNull(2) ? null : reader.GetString(2);
                    refreshToken = reader.IsDBNull(5) ? null : reader.GetString(5);
                }
            }

            if (
                !string.IsNullOrEmpty(uuid) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(refreshToken)
            )
            {
                return new User(
                    new Guid(uuid),
                    email,
                    password,
                    new Guid(refreshToken)
                );
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"error creating user: {exc.Message}");
            throw;
        }

        return null;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        await using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        string? uuid = null;
        string? email = null;
        string? password = null;
        string? refreshToken = null;

        try
        {
            string stmt = $"SELECT * FROM accounts WHERE uuid = '{id}'";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(stmt, conn))
            await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    uuid = reader.IsDBNull(4) ? null : reader.GetString(4);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    password = reader.IsDBNull(2) ? null : reader.GetString(2);
                    refreshToken = reader.IsDBNull(5) ? null : reader.GetString(5);
                }
            }

            if (
                !string.IsNullOrEmpty(uuid) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(refreshToken)
            )
            {
                return new User(
                    new Guid(uuid),
                    email,
                    password,
                    new Guid(refreshToken)
                );
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"error retrieving user: {exc.Message}");
            throw;
        }

        return null;
    }

    public async Task<User?> GetUserByEmailAsync(string inputEmail)
    {
        await using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        string? uuid = null;
        string? email = null;
        string? password = null;
        string? refreshToken = null;

        try
        {
            string stmt = $"SELECT * FROM accounts WHERE email = '{inputEmail}'";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(stmt, conn))
            await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    uuid = reader.IsDBNull(4) ? null : reader.GetString(4);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    password = reader.IsDBNull(2) ? null : reader.GetString(2);
                    refreshToken = reader.IsDBNull(5) ? null : reader.GetString(5);
                }
            }

            if (
                !string.IsNullOrEmpty(uuid) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(refreshToken)
            )
            {
                return new User(
                    new Guid(uuid),
                    email,
                    password,
                    new Guid(refreshToken)
                );
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"error retrieving user: {exc.Message}");
            throw;
        }

        return null;
    }

    public async Task<User?> UpdateUserAsync(Guid inputUuid, string inputEmail, string inputPassword)
    {
        await using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();
        string? uuid = null;
        string? email = null;
        string? password = null;
        string? refreshToken = null;

        try
        {
            string insert =
                $"UPDATE accounts SET email = '{inputEmail}', password = '{inputPassword}' WHERE uuid = '{inputUuid}'";
            string select = $"SELECT * FROM accounts WHERE uuid = '{inputUuid}'";
            await using NpgsqlBatch batch = new NpgsqlBatch(conn)
            {
                BatchCommands =
                {
                    new NpgsqlBatchCommand(insert),
                    new NpgsqlBatchCommand(select)
                }
            };
            await using (NpgsqlDataReader reader = await batch.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    uuid = reader.IsDBNull(4) ? null : reader.GetString(4);
                    email = reader.IsDBNull(1) ? null : reader.GetString(1);
                    password = reader.IsDBNull(2) ? null : reader.GetString(2);
                    refreshToken = reader.IsDBNull(5) ? null : reader.GetString(5);
                }
            }

            if (
                !string.IsNullOrEmpty(uuid) &&
                !string.IsNullOrEmpty(email) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(refreshToken)
                )
            {
                return new User(
                    new Guid(uuid),
                    email,
                    password,
                    new Guid(refreshToken)
                );
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine($"error updating user: {exc.Message}");
            throw;
        }

        return null;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        await using NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
        await conn.OpenAsync();

        try
        {
            string delete = $"DELETE FROM accounts WHERE uuid = '{id}' RETURNING id";
            await using NpgsqlCommand cmd = new NpgsqlCommand(delete, conn);
            await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    int deletedId = reader.GetInt32(0);
                    Console.WriteLine($"deleted: {deletedId}");
                    return true;
                }
                else
                {
                    Console.WriteLine("failed to delete user");
                    return false;
                }
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc);
            throw;
        }
    }
}
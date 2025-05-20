using Microsoft.Data.Sqlite;
using System.Text.Json;
using VitalSignsStorage.Models;
using VitalSignsStorage.Persistence.Interfaces;

namespace VitalSignsStorage.Persistence.Repositories;

public class VitalDataRepository : IVitalDataRepository
{
    private readonly string _connectionString;

    public VitalDataRepository(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
        EnsureDatabaseCreated();
    }

    private void EnsureDatabaseCreated()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        CREATE TABLE IF NOT EXISTS VitalData (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Type TEXT NOT NULL,
            Value REAL NOT NULL,
            Timestamp TEXT NOT NULL,
            IsValid INTEGER NOT NULL
        );
        ";
        command.ExecuteNonQuery();
    }

    public async Task SaveAsync(VitalData data)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
        INSERT INTO VitalData (Type, Value, Timestamp, IsValid)
        VALUES ($type, $value, $timestamp, $isValid);
        ";

        command.Parameters.AddWithValue("$type", data.Type.ToString());
        command.Parameters.AddWithValue("$value", data.Value);
        command.Parameters.AddWithValue("$timestamp", data.Timestamp.ToString("o"));
        command.Parameters.AddWithValue("$isValid", data.IsValid ? 1 : 0);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<VitalData>> GetAllAsync()
    {
        var results = new List<VitalData>();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Type, Value, Timestamp, IsValid FROM VitalData";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var type = Enum.Parse<VitalType>(reader.GetString(0));
            var value = reader.GetDouble(1);
            var timestamp = DateTime.Parse(reader.GetString(2));
            var isValid = reader.GetInt32(3) == 1;

            results.Add(new VitalData
            {
                Type = type,
                Value = value,
                Timestamp = timestamp,
                IsValid = isValid
            });
        }

        return results;
    }
}

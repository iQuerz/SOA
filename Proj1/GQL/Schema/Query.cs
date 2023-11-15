using Microsoft.Data.Sqlite;

namespace GQL.Schema
{
    public class Query
    {
        readonly string connectionString = "Data Source=../Database/iot_telemetry_data.db;";

        public async Task<List<SensorReadingType>> GetReadings(int take = 10, string device = "", bool sort = true)
        {
            var result = new List<SensorReadingType>();

            var where = string.IsNullOrEmpty(device) ? string.Empty : $"WHERE device='{device}'";
            var limit = $"LIMIT {take}";
            var order = sort ? $"ORDER BY ts ASC" : "";

            string query = $"SELECT * FROM iot_telemetry_data" +
                           $"\n{where}" +
                           $"\n{order}" +
                           $"\n{limit}";

            SQLitePCL.Batteries.Init();

            using (SqliteConnection connection = new(connectionString))
            {
                await connection.OpenAsync();
                using SqliteCommand command = new(query, connection);
                using SqliteDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var i = 0;
                    result.Add(new SensorReadingType
                    {
                        ts = reader.GetString(i++),
                        device = reader.GetString(i++),
                        co = reader.GetDouble(i++),
                        humidity = reader.GetDouble(i++),
                        light = reader.GetBoolean(i++),
                        lpg = reader.GetDouble(i++),
                        motion = reader.GetBoolean(i++),
                        smoke = reader.GetDouble(i++),
                        temp = reader.GetDouble(i++),
                    });
                }
            }

            return result;
        }
    }
}

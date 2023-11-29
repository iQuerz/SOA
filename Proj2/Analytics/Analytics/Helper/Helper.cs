using Analytics.Model;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System.Globalization;

namespace Analytics.Helper
{
    public class Helper
    {
        public static IoTReading Parse(string input)
        {
            string[] fields = input.Split(',');

            if (fields.Length != 9)
            {
                throw new ArgumentException("Invalid input string format");
            }
            if (!double.TryParse(fields[2].Trim('"'), NumberStyles.Float, CultureInfo.InvariantCulture, out double co) ||
                !double.TryParse(fields[3].Trim('"'), NumberStyles.Float, CultureInfo.InvariantCulture, out double humidity) ||
                !bool.TryParse(fields[4].Trim('"'), out bool light) ||
                !double.TryParse(fields[5].Trim('"'), NumberStyles.Float, CultureInfo.InvariantCulture, out double lpg) ||
                !bool.TryParse(fields[6].Trim('"'), out bool motion) ||
                !double.TryParse(fields[7].Trim('"'), NumberStyles.Float, CultureInfo.InvariantCulture, out double smoke) ||
                !double.TryParse(fields[8].Trim('"'), NumberStyles.Float, CultureInfo.InvariantCulture, out double temp))
            {
                throw new ArgumentException("Invalid numeric value in the input string");
            }

            IoTReading myData = new()
            {
                Ts = fields[0].Trim('"'),
                Device = fields[1].Trim('"'),
                Co = co,
                Humidity = humidity,
                Light = light,
                Lpg = lpg,
                Motion = motion,
                Smoke = smoke,
                Temp = temp
            };

            return myData;
        }
        public async Task WriteToDatabase(IoTReading received, InfluxDBClient client)
        {
            var point = PointData
                .Measurement("sensor")
                .Tag("timestamp", received.Ts.ToString())
                .Field("device", received.Device.ToString())
                .Field("co", received.Co.ToString())
                .Field("humidity", received.Humidity.ToString())
                .Field("light", received.Light.ToString())
                .Field("lpg", received.Lpg.ToString())
                .Field("motion", received.Motion.ToString())
                .Field("smoke", received.Smoke.ToString())
                .Field("temp", received.Temp.ToString())
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            Console.WriteLine($"Write in InfluxDb check");

            await client.GetWriteApiAsync().WritePointAsync(point, "IoTs", "ventilatori");
            Console.WriteLine($"Write in InfluxDb: sensor");

        }
    }
}

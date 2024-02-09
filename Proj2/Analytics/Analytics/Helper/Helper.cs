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
            string appended = input + ",end";
            string[] fields = appended.Split(',');
            string lastCaracter = fields[8];
            lastCaracter = lastCaracter.Substring(0, lastCaracter.Length - 1);



            if (fields.Length != 10)
            {
                throw new ArgumentException("Invalid input string format");
            }

            if (!double.TryParse(fields[2].Trim('"'), out double co))
            {
                throw new ArgumentException($"Invalid numeric value for 'co': {fields[2]}");
            }

            if (!double.TryParse(fields[3].Trim('"'), out double humidity))
            {
                throw new ArgumentException($"Invalid numeric value for 'humidity': {fields[3]}");
            }

            if (!bool.TryParse(fields[4].Trim('"'), out bool light))
            {
                throw new ArgumentException($"Invalid boolean value for 'light': {fields[4]}");
            }

            if (!double.TryParse(fields[5].Trim('"'), out double lpg))
            {
                throw new ArgumentException($"Invalid numeric value for 'lpg': {fields[5]}");
            }

            if (!bool.TryParse(fields[6].Trim('"'), out bool motion))
            {
                throw new ArgumentException($"Invalid boolean value for 'motion': {fields[6]}");
            }

            if (!double.TryParse(fields[7].Trim('"'), out double smoke))
            {
                throw new ArgumentException($"Invalid numeric value for 'smoke': {fields[7]}");
            }

            if (!double.TryParse(lastCaracter.Trim('"'), out double temp))
            {
                throw new ArgumentException($"Invalid numeric value for 'temp': {fields[8]}");
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

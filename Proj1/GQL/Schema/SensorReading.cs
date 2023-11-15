namespace GQL.Schema
{
    public class SensorReadingType
    {
        public string ts {  get; set; }
        public string date => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(Convert.ToDouble(ts)).ToLocalTime().ToString("HH:mm dd.MM.yyyy");
        public string device { get; set; }
        public double co { get; set; }
        public double humidity { get; set; }
        public bool light { get; set; }
        public double lpg { get; set; }
        public bool motion { get; set; }
        public double smoke { get; set; }
        public double temp { get; set; }
    }
}

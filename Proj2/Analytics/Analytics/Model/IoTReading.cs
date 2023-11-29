using System.Xml.Linq;

namespace Analytics.Model
{
    public class IoTReading
    {
            public required string Ts { get; set; }
            public required string Device { get; set; }
            public double Co { get; set; }
            public double Humidity { get; set; }
            public bool Light { get; set; }
            public double Lpg { get; set; }
            public bool Motion { get; set; }
            public double Smoke { get; set; }
            public double Temp { get; set; }
    }

}

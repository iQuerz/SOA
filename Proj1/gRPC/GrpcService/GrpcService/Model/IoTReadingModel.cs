using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcService.Model
{

    [Table("iot_telemetry_data")]
    [PrimaryKey(nameof(Ts), nameof(Device))]
    public class IoTReadingModel
    {
        [Column(Order = 0)]
        public required string Ts { get; set; }
        [Column(Order = 1)]
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

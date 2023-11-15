using System.ComponentModel.DataAnnotations.Schema;

namespace GrpcService.Model
{

    [Table("IOTMeterData")]
    public class IoTReadingModel
    {
        public int Id { get; set; }
        [Column("Temperature(C)")]
        public double Temperature { get; set; }
        [Column("pH")]
        public double PH { get; set; }
        [Column("Turbidity(NTU)")]
        public double Turbidity { get; set; }
        [Column("BOD(mg/l)")]
        public double BOD { get; set; }
        [Column("FecalColiform(MPN/100ml)")]
        public int FecalColiform { get; set; }
        [Column("DisolvedOxygen(mg/l)")]
        public double DisolvedOxygen { get; set; }
        [Column("NITRATENANN+NITRITENANN(mg/l)")]
        public double Nitratenans { get; set; }
        [Column("Conductivity(micro_mhos/cm)")]
        public int Conductivity { get; set; }
    }
}

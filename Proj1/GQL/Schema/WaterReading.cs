namespace GQL.Schema
{
    public class WaterReadingType
    {
        public double Temperature { get; set; }
        public double PH { get; set; }
        public double Turbidity { get; set; }
        public double BOD { get; set; }
        public int FecalColiform { get; set; }
        public double DisolvedOxygen { get; set; }
        public double Nitratenans { get; set; }
        public int Conductivity { get; set; }
    }
}

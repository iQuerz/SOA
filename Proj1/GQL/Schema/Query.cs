using Bogus;

namespace GQL.Schema
{
    public class Query
    {
        public IEnumerable<WaterReadingType> GetReadings()
        {
            Faker<WaterReadingType> faker = new();
            return faker.Generate(5);
        }

        public IEnumerable<WaterReadingType> GetReadingsByFilter()
        {
            Faker<WaterReadingType> faker = new();
            return faker.Generate(5);
        }
    }
}

using GrpcService.Model;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Data
{
    public class IoTDbContext : DbContext
    {
        public DbSet<IoTReadingModel> iot_telemetry_data { get; set; }

        public IoTDbContext(DbContextOptions<IoTDbContext> options) : base(options)
        {
        }
    }
}

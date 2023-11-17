using Grpc.Core;
using GrpcService;
using GrpcService.Data;
using GrpcService.Model;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IoTDbContext _dbContext;
        public GreeterService(ILogger<GreeterService> logger, IoTDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        //helper function
        public static IoTReading ToGrpcMessage(IoTReadingModel readingModel)
        {
            return new IoTReading
            {
                // Maping properties from model to gRPC message
                Ts = readingModel.Ts,
                Device = readingModel.Device,
                Co = (float)readingModel.Co,
                Humidity = (float)readingModel.Humidity,
                Light = (bool)readingModel.Light,
                Lpg = (float)readingModel.Lpg,
                Motion = (bool)readingModel.Motion,
                Smoke = (float)readingModel.Smoke,
                Temp = (float)readingModel.Temp,
            };
        }

        public override Task<IoTList> GetAll(IoTParams request, ServerCallContext context)
        {
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            //var readings = _dbContext.iot_telemetry_data.ToList();
            var query = _dbContext.iot_telemetry_data.AsQueryable();

            // Implement pagination
            var paginatedData = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var grpcList = new IoTList();
            grpcList.IoTReadings.AddRange(paginatedData.Select(ToGrpcMessage));

            return Task.FromResult(grpcList);
        }
        public override Task<IoTReading> Get(IoTId request, ServerCallContext context)
        {

            var reading = _dbContext.iot_telemetry_data.FirstOrDefault(r => r.Ts == request.Ts & r.Device == request.Device);

            if (reading != null)
            {
                var grpcReading = new IoTReading();
                grpcReading = ToGrpcMessage(reading);
                return Task.FromResult(grpcReading);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, " Reading not found"));
            }
        }
        public override Task<IoTReading> Create(IoTReading request, ServerCallContext context)
        {
            var newReading = new IoTReadingModel
            {
                // Maping properties from gRPC message to model
                Ts = request.Ts,
                Device = request.Device,
                Co = request.Co,
                Humidity = request.Humidity,
                Light = request.Light,
                Lpg = request.Lpg,
                Motion = request.Motion,
                Smoke = request.Smoke,
                Temp = request.Temp
            };
            _dbContext.iot_telemetry_data.Add(newReading);
            if (_dbContext.SaveChanges() > 0)
            {
                return Task.FromResult(request);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.Aborted, " Reading could not be saved")); ;
            }
        }
        public override Task<IoTReading> Update(IoTReading request, ServerCallContext context)
        {
            var existingReading = _dbContext.iot_telemetry_data.FirstOrDefault(r => r.Ts == request.Ts & r.Device == request.Device);
            if (existingReading != null)
            {
                // Maping properties from gRPC message to model
                existingReading.Ts = request.Ts;
                existingReading.Device = request.Device;
                existingReading.Co = request.Co;
                existingReading.Humidity = request.Humidity;
                existingReading.Light = request.Light;
                existingReading.Lpg = request.Lpg;
                existingReading.Motion = request.Motion;
                existingReading.Smoke = request.Smoke;
                existingReading.Temp = request.Temp;

                _dbContext.SaveChanges();

                return Task.FromResult(request);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, " Reading not found")); ;
            }
        }
        public override Task<IoTReading> Delete(IoTId request, ServerCallContext context)
        {

            var reading = _dbContext.iot_telemetry_data.FirstOrDefault(r => r.Ts == request.Ts & r.Device == request.Device);

            if (reading != null)
            {
                var grpcReading = new IoTReading();
                grpcReading = ToGrpcMessage(reading);

                _dbContext.iot_telemetry_data.Remove(reading);

                _dbContext.SaveChanges();

                return Task.FromResult(grpcReading);
            }
            else
            {
                throw new RpcException(new Status(StatusCode.NotFound, " Reading not found"));
            }
        }
    }
}
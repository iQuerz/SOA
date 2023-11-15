using Grpc.Core;
using GrpcService;
using GrpcService.Data;
using GrpcService.Model;
using Microsoft.EntityFrameworkCore;

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
                Temperature = (float)readingModel.Temperature,
                PH = (float)readingModel.PH,
                Turbidity = (float)readingModel.Turbidity,
                BOD = (float)readingModel.BOD,
                FecalColiform = readingModel.FecalColiform,
                DisolvedOxygen = (float)readingModel.DisolvedOxygen,
                Nitratenans = (float)readingModel.Nitratenans,
                Conductivity = readingModel.Conductivity,
            };
        }

        public override Task<IoTList> GetAll(IoTParams request, ServerCallContext context)
        {

            var readings = _dbContext.IOTMeterData.ToList();

            var grpcList = new IoTList();
            grpcList.IoTReadings.AddRange(readings.Select(ToGrpcMessage));

            return Task.FromResult(grpcList);
        }
        public override Task<IoTReading> Get(IoTId request, ServerCallContext context)
        {

            var reading = _dbContext.IOTMeterData.FirstOrDefault(r => r.Id == request.Id);
            
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
                Temperature = request.Temperature,
                PH = request.PH,
                Turbidity = request.Turbidity,
                BOD = request.BOD,
                FecalColiform = request.FecalColiform,
                DisolvedOxygen = request.DisolvedOxygen,
                Nitratenans = request.Nitratenans,
                Conductivity = request.Conductivity
            };
            _dbContext.IOTMeterData.Add(newReading);
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
            var existingReading = _dbContext.IOTMeterData.FirstOrDefault(r => r.Id == request.Id);
            if (existingReading != null)
            {
                // Maping properties from gRPC message to model
                existingReading.Temperature = request.Temperature;
                existingReading.PH = request.PH;
                existingReading.Turbidity = request.Turbidity;
                existingReading.BOD = request.BOD;
                existingReading.FecalColiform = request.FecalColiform;
                existingReading.DisolvedOxygen = request.DisolvedOxygen;
                existingReading.Nitratenans = request.Nitratenans;
                existingReading.Conductivity = request.Conductivity;

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

            var reading = _dbContext.IOTMeterData.FirstOrDefault(r => r.Id == request.Id);

            if (reading != null)
            {
                var grpcReading = new IoTReading();
                grpcReading = ToGrpcMessage(reading);
                
                _dbContext.IOTMeterData.Remove(reading);

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
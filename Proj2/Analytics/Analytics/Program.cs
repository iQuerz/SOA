using Analytics.Services;
using MQTTnet.Client;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mqttService = new MqttService();
await mqttService.ConnectAsync("mqtt-broker", 1883, "analyticsUser", "123");
await mqttService.SubscribeAsync("senzorski_podaci");
await mqttService.SubscribeAsync("analyzed_sensordata");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

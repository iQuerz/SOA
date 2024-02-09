using Visualization.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mqttService = new MqttService();
await mqttService.ConnectAsync("mqtt-broker_edgex", 1883, "user3", "123");
await mqttService.SubscribeAsync("senzorski_podaci_edgex");
//await mqttService.SubscribeAsync("Comand_edgex");

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

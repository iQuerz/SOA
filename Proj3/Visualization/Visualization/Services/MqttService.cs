using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Server;
using System.Text;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Analytics.Model;

namespace Visualization.Services
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;
        private MqttFactory mqttFactory = new MqttFactory();
        public InfluxDBClient _influxClient = InfluxDBClientFactory.Create(url: "http://influxdb_edgex:8086", token: "1234567890originalpassword");
        public MqttService()
        {
            _mqttClient = mqttFactory.CreateMqttClient();
        }

        public async Task ConnectAsync(string brokerAddress, int port, string username, string password)
        {
            try
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(brokerAddress, port)
                    .WithCredentials(username, password)
                    .Build();

                _mqttClient.ApplicationMessageReceivedAsync += async e =>
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                    Console.WriteLine("Received application message from topic : " + e.ApplicationMessage.Topic);
                    Console.WriteLine(payload);
                     if (e.ApplicationMessage.Topic == "senzorski_podaci_edgex")
                        {
                            var data = (JObject)JsonConvert.DeserializeObject(payload);
                            Console.WriteLine("data is : " + data);
                            string deviceValue = data["device"]?.ToString();
                            Console.WriteLine("deviceValue is : " + deviceValue);


                            string tempvalue = data["readings"]?[0]?["value"]?.ToString();
                            string cleanedString = tempvalue.Replace("\\", "").Replace("\"", "");
                            float temp = float.Parse(cleanedString);
                            Console.WriteLine("Temp in float is : " + temp);

                            var point = PointData
                                    .Measurement("sensor")
                                    .Tag("device", deviceValue)
                                    .Field("temp", temp)
                                    .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                            Console.WriteLine($"Write in InfluxDb check");
                        try
                        {
                            await _influxClient.GetWriteApiAsync().WritePointAsync(point, "IoTs", "Ventilatori");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error writing to InfluxDB: {ex.Message}");
                            // Log the exception or take appropriate action.
                        }
                        Console.WriteLine($"Write in InfluxDb: sensor");
                     }


                };

                await _mqttClient.ConnectAsync(options, CancellationToken.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during MQTT connection: {ex.Message}");
            }
        }

        public async Task SubscribeAsync(string topic)
        {
            try
            {
                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic(topic);
                        })
                    .Build();

                await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                Console.WriteLine("MQTT client subscribed to topic.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during MQTT subscription: {ex.Message}");
            }
        }

        //not needed for this project legecy code from last one
        public async Task PublishAsyncHelper(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message);
            Console.WriteLine("Sent application message from ekupier/sensordata.");
        }
        private void OnConnected(MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Connected to MQTT broker.");
        }

        private void OnDisconnected(MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Disconnected from MQTT broker.");
        }

        public async Task WriteToDatabase(IoTReading received)
        {
            var point = PointData
                .Measurement("sensor")
                .Tag("timestamp", received.Ts.ToString())
                .Field("device", received.Device.ToString())
                .Field("co", received.Co.ToString())
                .Field("humidity", received.Humidity.ToString())
                .Field("light", received.Light.ToString())
                .Field("lpg", received.Lpg.ToString())
                .Field("motion", received.Motion.ToString())
                .Field("smoke", received.Smoke.ToString())
                .Field("temp", received.Temp.ToString())
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            Console.WriteLine($"Write in InfluxDb check");

            await _influxClient.GetWriteApiAsync().WritePointAsync(point, "IoTs", "b82ad135e0225ab8");
            Console.WriteLine($"Write in InfluxDb: sensor");

        }
    }
}

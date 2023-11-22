using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Server;
using System.Text;

namespace Analytics.Services
{
    public class MqttService
    {
        private readonly IMqttClient _mqttClient;
        private MqttFactory mqttFactory = new MqttFactory();
        public MqttService()
        {
            _mqttClient = mqttFactory.CreateMqttClient();

        }

        public async Task ConnectAsync(string brokerAddress, int port)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(brokerAddress, port)
                .Build();

            _mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine("Received application message.");
                Console.WriteLine(payload);

                return Task.CompletedTask;
            };


            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }
        public async Task SubscribeAsync(string topic)
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

        public async Task PublishAsync(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithRetainFlag()
                .Build();

            await _mqttClient.PublishAsync(message);
        }
        private void OnConnected(MqttClientConnectedEventArgs e)
        {
            Console.WriteLine("Connected to MQTT broker.");
        }

        private void OnDisconnected(MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Disconnected from MQTT broker.");
        }
    }
}

using MQTTnet.Client;
using MQTTnet;
using MQTTnet.Server;


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

        public async Task ConnectAsync(string brokerAddress, int port, string clientId)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(brokerAddress, port)
                .WithClientId(clientId)
                .Build();


            await _mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public async Task SubscribeAsync(string messageCallback)
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("mqttnet/samples/topic/1");
                        })
                    .Build();

                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                Console.WriteLine("MQTT client subscribed to topic.");

                // The response contains additional data sent by the server after subscribing.
                Console.WriteLine(response);
            }
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
        private void OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            _messageCallback?.Invoke(e.ApplicationMessage.ConvertPayloadToString());
        }

        private Action<string> _messageCallback;
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

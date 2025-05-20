using MQTTnet;
using MQTTnet.Client;
using System.Text;
using VitalSignsStorage.Handlers;

namespace VitalSignsStorage.Communication;

public class MqttSubscriber
{
    private readonly string _brokerAddress = "localhost";
    private readonly int _brokerPort = 1883;
    private readonly string _topic = "vitals/data";

  public async Task StartAsync(VitalDataHandler handler)
{
    var mqttClient = new MqttFactory().CreateMqttClient();

    mqttClient.ApplicationMessageReceivedAsync += async e =>
    {
        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
        Console.WriteLine($"📩 Message received on topic {e.ApplicationMessage.Topic}: {payload}");
        await handler.HandleAsync(payload);
    };

    var options = new MqttClientOptionsBuilder()
        .WithTcpServer(_brokerAddress, _brokerPort)
        .WithClientId("VitalSignsReceiver")
        .WithCleanSession(false)
        .Build();

    mqttClient.ConnectedAsync += async e =>
    {
        Console.WriteLine("✅ Connected to MQTT broker.");
        await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(_topic)
            .WithAtLeastOnceQoS()
            .Build());
        Console.WriteLine($"📡 Subscribed to topic: {_topic}");
    };

    mqttClient.DisconnectedAsync += async e =>
    {
        Console.WriteLine("⚠️ Disconnected from MQTT broker. Retrying...");
        await Task.Delay(TimeSpan.FromSeconds(5));
        await mqttClient.ConnectAsync(options);
    };

    await mqttClient.ConnectAsync(options);
}

}

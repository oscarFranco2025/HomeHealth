using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Communication;

public class MqttClientSender : IMqttSender
{
    private readonly IMqttClient _mqttClient;
    private readonly string _topic;

    public MqttClientSender(string brokerHost, int port, string topic)
    {
        _topic = topic;

        var mqttFactory = new MQTTnet.MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerHost, port)
            .WithClientId("VitalSignsPublisher")
            .Build();

 
        _mqttClient.ConnectAsync(options, CancellationToken.None).GetAwaiter().GetResult();

        var clearMessage = new MqttApplicationMessageBuilder()
        .WithTopic(_topic)
        .WithPayload(Array.Empty<byte>()) // Mensaje vacío
        .WithRetainFlag(true)
        .Build();

    _mqttClient.PublishAsync(clearMessage, CancellationToken.None).GetAwaiter().GetResult();

    Console.WriteLine($"🧹 Retained message en '{_topic}' limpiado.");
    }

    public async Task SendAsync(VitalData data)
    {
        var payload = JsonSerializer.Serialize(data);


         Console.WriteLine($"🚀 Publicando en {_topic}: {payload}");
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(_topic)
            .WithPayload(Encoding.UTF8.GetBytes(payload))
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            .WithRetainFlag(true)
            .Build();

        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }
}

// This class implements the IMqttSender interface and is responsible for sending       

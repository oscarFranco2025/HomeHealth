using System.Text.Json;
using VitalSignsProcessor.Communication;
using VitalSignsProcessor.Filters;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Services;
public class SenderService
{
    private readonly IEnumerable<IFilter> _pipeline;
    private readonly IMqttSender _mqttSender;

    public SenderService(IEnumerable<IFilter> pipeline, IMqttSender mqttSender)
    {
        _pipeline = pipeline;
        _mqttSender = mqttSender;
    }

    public async Task SendAsync(VitalData raw)
    {
        var data = raw;
        foreach (var filter in _pipeline)
        {
            if (data == null) break;
            data = filter.Process(data);
        }

        if (data != null)
        {
            Console.WriteLine($"✅ Enviando dato válido: {JsonSerializer.Serialize(data)}");
            await _mqttSender.SendAsync(data);
        }
        else
        {
            Console.WriteLine("⚠️ Dato descartado por los filtros.");
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using VitalSignsProcessor.Communication;
using VitalSignsProcessor.Filters;
using VitalSignsProcessor.Services;

var services = new ServiceCollection();

// Registro de filtros individuales como IFilter (Pipe & Filters)
services.AddSingleton<IFilter, RawDataFilter>();
services.AddSingleton<IFilter, NoiseReducer>();
services.AddSingleton<IFilter, UnitNormalizer>();
services.AddSingleton<IFilter, VitalSignsValidator>();
services.AddSingleton<IFilter, AlertTrigger>();


services.AddSingleton<IMqttSender>(sp =>
    new MqttClientSender("localhost", 1883, "vitals/data"));

// Registro del servicio orquestador
services.AddSingleton<SenderService>();

// Construcción del contenedor
var provider = services.BuildServiceProvider();

// Simulación de lectura de datos
var sensor = new SensorSimulator();
var sender = provider.GetRequiredService<SenderService>();

for (int i = 0; i < 5; i++)
{
    var reading = sensor.GetReading();
    await sender.SendAsync(reading);
    await Task.Delay(1000);
}

using System.Text.Json;
using VitalSignsStorage.Models;
using VitalSignsStorage.Persistence.Interfaces;

namespace VitalSignsStorage.Handlers;

public class VitalDataHandler
{
    private readonly IVitalDataRepository _repository;

    public VitalDataHandler(IVitalDataRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Recibe un payload en JSON (desde MQTT), lo interpreta y lo almacena.
    /// </summary>
    /// <param name="payload">Mensaje JSON como string</param>
    public async Task HandleAsync(string payload)
    {
        try
        {
            var data = JsonSerializer.Deserialize<VitalData>(payload);

            if (data == null)
            {
                Console.WriteLine("❌ Payload no pudo deserializarse.");
                return;
            }

            Console.WriteLine($"📥 Recibido: {JsonSerializer.Serialize(data)}");

            await _repository.SaveAsync(data);

            Console.WriteLine($"✅ Guardado en BD: {data.Type}, valor: {data.Value}, válido: {data.IsValid}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al manejar el mensaje: {ex.Message}");
        }
    }
}

using VitalSignsStorage.Communication;
using VitalSignsStorage.Handlers;
using VitalSignsStorage.Persistence.Interfaces;
using VitalSignsStorage.Persistence.Repositories;

namespace HomeHealth.VitalSignsStorage;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("🩺 Vital Signs Storage Service starting...");

        // Ruta a la base de datos SQLite
        string dbPath = "vitalsigns.db";

        // Crear el repositorio (esto asegura la creación de la BD y tabla)
        IVitalDataRepository repository = new VitalDataRepository(dbPath);

        // Crear el manejador que usará el repositorio
        var handler = new VitalDataHandler(repository);

        var subscriber = new MqttSubscriber();
        await subscriber.StartAsync(handler); // 👈 Pasa el handler aquí directamente

 
        // Evita que se cierre la consola
        Console.WriteLine("🟢 Listening for MQTT messages...");
        await Task.Delay(Timeout.Infinite);
    }
}


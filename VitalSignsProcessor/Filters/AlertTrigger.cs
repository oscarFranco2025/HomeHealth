using System.Text.Json;
using VitalSignsProcessor.Filters;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;
public class AlertTrigger : IFilter
{
    public VitalData? Process(VitalData input)
    {
        Console.WriteLine($"[AlertTrigger] ENTRA: {JsonSerializer.Serialize(input)}");

        if (input.Type == VitalType.OxygenSaturation && input.Value < 90)
        {
            Console.WriteLine($"⚠️ ALERTA: Saturación baja: {input.Value}%");
        }

        Console.WriteLine($"[AlertTrigger] SALE: {JsonSerializer.Serialize(input)}");

        return input;
    }
}

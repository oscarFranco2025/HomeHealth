using System.Text.Json;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;


public class UnitNormalizer : IFilter
{
    public VitalData? Process(VitalData input)
    {
        Console.WriteLine($"[UnitNormalizer] ENTRA: {JsonSerializer.Serialize(input)}");

        if (input.Type == VitalType.Temperature)
        {
            input.Value = (input.Value - 32) * 5.0 / 9.0;
        }

        Console.WriteLine($"[UnitNormalizer] SALE: {JsonSerializer.Serialize(input)}");

        return input;
    }
}
// This filter normalizes the temperature value from Fahrenheit to Celsius. 
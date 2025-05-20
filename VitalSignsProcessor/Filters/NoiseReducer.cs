using System.Text.Json;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;

public class NoiseReducer : IFilter
{
    public VitalData? Process(VitalData input)
    {
        Console.WriteLine($"[NoiseReducer] ENTRA: {JsonSerializer.Serialize(input)}");

        input.Value = Math.Round(input.Value, 1);

        Console.WriteLine($"[NoiseReducer] SALE: {JsonSerializer.Serialize(input)}");

        return input;
    }
}
// This filter reduces noise in the data by rounding the value to one decimal place.    
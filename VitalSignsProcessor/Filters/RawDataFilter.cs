using System.Text.Json;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;

public class RawDataFilter : IFilter
{
    public VitalData Process(VitalData input)
    {
        Console.WriteLine($"[RawDataFilter] ENTRA: {JsonSerializer.Serialize(input)}");
        
        input.IsValid = input.Value > 0;
        VitalData? result = input.IsValid ? input : new VitalData { IsValid = false, Value = 0 };

        Console.WriteLine($"[RawDataFilter] SALE: {JsonSerializer.Serialize(result)}");

        return input.IsValid ? input : new VitalData { IsValid = false, Value = 0 };
    }
}
// This filter checks if the input data is valid. If it is, it returns the input data; otherwise, it returns a new instance of VitalData.   
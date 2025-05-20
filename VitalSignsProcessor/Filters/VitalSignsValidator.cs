using System.Text.Json;
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;
public class VitalSignsValidator : IFilter
{
    public VitalData? Process(VitalData input)
    {
        Console.WriteLine($"[VitalSignsValidator] ENTRA: {JsonSerializer.Serialize(input)}");

        if (input.Type == VitalType.HeartRate)
        {
            input.IsValid = input.Value is > 40 and < 180;
        }

        VitalData? result = input.IsValid ? input : null;

        Console.WriteLine($"[VitalSignsValidator] SALE: {JsonSerializer.Serialize(result)}");

        return result;
    }
}
// This filter validates the heart rate value. If it is within the range of 40 to 180, it marks it as valid; otherwise, it marks it as invalid.
// The Process method returns the input data if valid; otherwise, it returns null.
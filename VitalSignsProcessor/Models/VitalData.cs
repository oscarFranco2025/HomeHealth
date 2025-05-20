namespace VitalSignsProcessor.Models;

public enum VitalType { Temperature, HeartRate, OxygenSaturation }

public class VitalData
{
    public VitalType Type { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsValid { get; set; } = true;

    public override string ToString()
        => $"[{Type}] {Value} at {Timestamp:HH:mm:ss}";

}

using VitalSignsProcessor.Models;

public class SensorSimulator
{
    private static readonly Random rnd = new();

    public VitalData GetReading()
    {
        var type = (VitalType)rnd.Next(0, 3); // Random entre 0 (Temperature), 1 (HeartRate), 2 (OxygenSaturation)
        double value = type switch
        {
            VitalType.Temperature => 95 + rnd.NextDouble() * 10,        // 95°F – 105°F
            VitalType.HeartRate => 60 + rnd.NextDouble() * 100,         // 60–160 bpm
            VitalType.OxygenSaturation => 85 + rnd.NextDouble() * 15,   // 85–100 %
            _ => 0
        };

        return new VitalData
        {
            Type = type,
            Value = value
        };
    }
}

// This class simulates a sensor that generates random heart rate readings.
// It uses a random number generator to create a heart rate value between 60 and 140 beats per minute.
using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Communication;

public interface IMqttSender
{
    Task SendAsync(VitalData data);
}

// This interface defines a contract for sending vital data asynchronously.
// It includes a method SendAsync that takes a VitalData object as a parameter.
// The implementation of this interface would handle the actual sending of the data,
// such as through MQTT or another communication protocol.  
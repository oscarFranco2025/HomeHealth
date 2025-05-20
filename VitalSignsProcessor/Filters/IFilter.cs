using VitalSignsProcessor.Models;

namespace VitalSignsProcessor.Filters;

public interface IFilter
{
    VitalData? Process(VitalData input);
}
// This interface defines a contract for processing vital data.
// It includes a method Process that takes a VitalData object as a parameter.   


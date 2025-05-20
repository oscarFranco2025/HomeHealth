// File: Persistence/Interfaces/IVitalDataRepository.cs
using VitalSignsStorage.Models;

namespace VitalSignsStorage.Persistence.Interfaces;

public interface IVitalDataRepository
{
    Task SaveAsync(VitalData data);
    Task<IEnumerable<VitalData>> GetAllAsync();
}

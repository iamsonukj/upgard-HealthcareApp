using HealthcareApp.BLL.DTOs;

namespace HealthcareApp.BLL.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllAsync();
        Task<PatientDto> GetByIdAsync(int id);
        Task AddAsync(PatientDto dto);
        Task UpdateAsync(PatientDto dto);
        Task DeleteAsync(int id);
    }
}
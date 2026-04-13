using HealthcareApp.BLL.DTOs;

namespace HealthcareApp.BLL.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllAsync();
        Task<DoctorDto> GetByIdAsync(int id);
        Task AddAsync(DoctorDto dto);
        Task UpdateAsync(DoctorDto dto);
        Task DeleteAsync(int id);
    }
}
using HealthcareApp.BLL.DTOs;

namespace HealthcareApp.BLL.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto> GetByIdAsync(int id);
        Task AddAsync(AppointmentDto dto);
        Task UpdateAsync(AppointmentDto dto);
        Task DeleteAsync(int id);
    }
}
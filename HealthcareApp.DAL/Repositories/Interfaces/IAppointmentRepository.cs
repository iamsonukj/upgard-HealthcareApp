using HealthcareApp.Entities.Models;

namespace HealthcareApp.DAL.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);

        Task<bool> IsSlotBooked(int doctorId, DateTime date, string timeSlot);
    }
}
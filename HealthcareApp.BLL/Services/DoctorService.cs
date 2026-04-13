using HealthcareApp.BLL.DTOs;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.DAL.Repositories.Interfaces;
using HealthcareApp.Entities.Models;

namespace HealthcareApp.BLL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

  
        public async Task<List<DoctorDto>> GetAllAsync()
        {
            var doctors = await _repo.GetAllAsync();

            return doctors.Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Specialization = d.Specialization,
                AvailableTimeSlot = d.AvailableTimeSlot
            }).ToList();
        }

       
        public async Task<DoctorDto> GetByIdAsync(int id)
        {
            var d = await _repo.GetByIdAsync(id);

            if (d == null) return null;

            return new DoctorDto
            {
                DoctorId = d.DoctorId,
                Name = d.Name,
                Specialization = d.Specialization,
                AvailableTimeSlot = d.AvailableTimeSlot
            };
        }

        // ADD
        public async Task AddAsync(DoctorDto dto)
        {
            var entity = new Doctor
            {
                Name = dto.Name,
                Specialization = dto.Specialization,
                AvailableTimeSlot = dto.AvailableTimeSlot
            };

            await _repo.AddAsync(entity);
        }

        // UPDATE
        public async Task UpdateAsync(DoctorDto dto)
        {
            var entity = new Doctor
            {
                DoctorId = dto.DoctorId,
                Name = dto.Name,
                Specialization = dto.Specialization,
                AvailableTimeSlot = dto.AvailableTimeSlot
            };

            await _repo.UpdateAsync(entity);
        }

        //  DELETE
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
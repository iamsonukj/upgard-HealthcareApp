using HealthcareApp.BLL.DTOs;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.DAL.Repositories.Interfaces;
using HealthcareApp.Entities.Models;

namespace HealthcareApp.BLL.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return data.Select(p => new PatientDto
            {
                PatientId = p.PatientId,
                Name = p.Name,
                Age = p.Age,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                MedicalNotes = p.MedicalNotes
            }).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new PatientDto
            {
                PatientId = p.PatientId,
                Name = p.Name,
                Age = p.Age,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                MedicalNotes = p.MedicalNotes
            };
        }

        public async Task AddAsync(PatientDto dto)
        {
            var entity = new Patient
            {
                Name = dto.Name,
                Age = dto.Age,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                MedicalNotes = dto.MedicalNotes
            };

            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(PatientDto dto)
        {
            var entity = new Patient
            {
                PatientId = dto.PatientId,
                Name = dto.Name,
                Age = dto.Age,
                Gender = dto.Gender,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                MedicalNotes = dto.MedicalNotes
            };

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
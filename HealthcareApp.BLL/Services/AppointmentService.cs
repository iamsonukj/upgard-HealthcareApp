using HealthcareApp.BLL.DTOs;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.DAL.Repositories.Interfaces;
using HealthcareApp.Entities.Models;

namespace HealthcareApp.BLL.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IDoctorRepository _doctorRepo;

        public AppointmentService(
            IAppointmentRepository appointmentRepo,
            IPatientRepository patientRepo,
            IDoctorRepository doctorRepo)
           
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
        }

        // Add
        public async Task AddAsync(AppointmentDto dto)
        {
            if (dto == null)
                throw new Exception("Appointment data is required");

            if (dto.Date.Date < DateTime.Today)
                throw new Exception("Appointment date must be today or future date");


            var patient = await _patientRepo.GetByIdAsync(dto.PatientId);
            if (patient == null)
                throw new Exception("Invalid patient selected");

            var doctor = await _doctorRepo.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
                throw new Exception("Invalid doctor selected");

            var timeSlot = dto.TimeSlot?.Trim();
            if (string.IsNullOrWhiteSpace(timeSlot))
                throw new Exception("Time slot is required");

            //  Doctor slot validation
            var doctorSlot = doctor.AvailableTimeSlot?.Trim();
            if (!string.Equals(doctorSlot, timeSlot, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Doctor is available only at {doctorSlot}");

            //  Duplicate booking check
            var isSlotBooked = await _appointmentRepo.IsSlotBooked( dto.DoctorId, dto.Date.Date,timeSlot);

            if (isSlotBooked)
                throw new Exception("This doctor already has an appointment for the selected date and time slot");

            var entity = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date.Date,
                TimeSlot = timeSlot,
                Status = dto.Status
            };

            await _appointmentRepo.AddAsync(entity);
        }

       
        public async Task<List<AppointmentDto>> GetAllAsync()
        {
            var list = await _appointmentRepo.GetAllAsync();

            return list.Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PatientName = a.Patient?.Name ?? "",
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.Name ?? "",
                Date = a.Date,
                TimeSlot = a.TimeSlot,
                Status = a.Status
            }).ToList();
        }

      
        public async Task<AppointmentDto> GetByIdAsync(int id)
        {
            var a = await _appointmentRepo.GetByIdAsync(id);

            if (a == null)
                throw new Exception("Appointment not found");

            return new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                PatientName = a.Patient?.Name ?? "",
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.Name ?? "",
                Date = a.Date,
                TimeSlot = a.TimeSlot,
                Status = a.Status
            };
        }

        // Update
        public async Task UpdateAsync(AppointmentDto dto)
        {
            if (dto == null)
                throw new Exception("Invalid appointment data");

            if (dto.Date.Date < DateTime.Today)
                throw new Exception("Appointment date must be in the future");

            var patient = await _patientRepo.GetByIdAsync(dto.PatientId);
            if (patient == null)
                throw new Exception("Invalid patient selected");

            var doctor = await _doctorRepo.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
                throw new Exception("Invalid doctor selected");

            var timeSlot = dto.TimeSlot?.Trim();
            if (string.IsNullOrWhiteSpace(timeSlot))
                throw new Exception("Time slot is required");

            var doctorSlot = doctor.AvailableTimeSlot?.Trim();
            if (!string.Equals(doctorSlot, timeSlot, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Doctor is available only at {doctorSlot}");

            var entity = new Appointment
            {
                AppointmentId = dto.AppointmentId,
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date.Date,
                TimeSlot = timeSlot,
                Status = dto.Status
            };

            await _appointmentRepo.UpdateAsync(entity);
        }

       
        public async Task DeleteAsync(int id)
        {
            await _appointmentRepo.DeleteAsync(id);
        }
    }
}
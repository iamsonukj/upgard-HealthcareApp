using HealthcareApp.Entities.Enums;

namespace HealthcareApp.Entities.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = "";

        public AppointmentStatus Status { get; set; }
    }
}
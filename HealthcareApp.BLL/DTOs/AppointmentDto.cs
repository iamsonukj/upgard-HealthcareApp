using HealthcareApp.Entities.Enums;
namespace HealthcareApp.BLL.DTOs
{

    public class AppointmentDto
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; } = "";

        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = "";

        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = "";

        public AppointmentStatus Status { get; set; }
    }
}
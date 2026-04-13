namespace HealthcareApp.Entities.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = "";
        public string Specialization { get; set; } = "";
        public string AvailableTimeSlot { get; set; } = "";

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using HealthcareApp.Entities.Enums;

namespace HealthcareApp.Web.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = string.Empty;

        public AppointmentStatus Status { get; set; }

        // Dropdowns
        public List<SelectListItem>? Patients { get; set; }
        public List<SelectListItem>? Doctors { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.BLL.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Specialization { get; set; }

        [Required]
        public string? AvailableTimeSlot { get; set; }
    }
}
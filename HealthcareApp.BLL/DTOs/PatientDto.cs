using System.ComponentModel.DataAnnotations;

namespace HealthcareApp.BLL.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;


        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;


        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone must be 10 digits")]
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [StringLength(500)]
        public string? MedicalNotes { get; set; }
    }
}
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HealthcareApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IAppointmentService _appointmentService;

        public HomeController(
            IPatientService patientService,
            IDoctorService doctorService,
            IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _doctorService = doctorService;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel
            {
                TotalPatients = (await _patientService.GetAllAsync()).Count(),
                TotalDoctors = (await _doctorService.GetAllAsync()).Count(),
                TotalAppointments = (await _appointmentService.GetAllAsync()).Count()
            };

            return View(model);
        }
    }
}
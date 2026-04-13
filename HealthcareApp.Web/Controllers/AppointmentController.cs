using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.BLL.DTOs;
using HealthcareApp.Web.ViewModels;

namespace HealthcareApp.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(
            IAppointmentService service,
            IPatientService patientService,
            IDoctorService doctorService)
        {
            _service = service;
            _patientService = patientService;
            _doctorService = doctorService;
        }

      
        public async Task<IActionResult> Index(DateTime? date, string status)
        {
            var data = await _service.GetAllAsync();

            // Filter by Date
            if (date.HasValue)
            {
                data = data.Where(a => a.Date.Date == date.Value.Date).ToList();
            }

            // Filter by Status
            if (!string.IsNullOrEmpty(status))
            {
                data = data.Where(a => a.Status.ToString() == status).ToList();
            }

            ViewBag.SelectedDate = date?.ToString("yyyy-MM-dd");
            ViewBag.SelectedStatus = status;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new AppointmentViewModel
            {
                Date = DateTime.Today.AddDays(1) 
            };

            return View(await LoadDropdowns(vm));
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(await LoadDropdowns(vm));

                await _service.AddAsync(new AppointmentDto
                {
                    PatientId = vm.PatientId,
                    DoctorId = vm.DoctorId,
                    Date = vm.Date,
                    TimeSlot = vm.TimeSlot,
                    Status = vm.Status
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", ex.Message);

                return View(await LoadDropdowns(vm));
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            var vm = new AppointmentViewModel
            {
                AppointmentId = data.AppointmentId,
                PatientId = data.PatientId,
                DoctorId = data.DoctorId,
                Date = data.Date,
                TimeSlot = data.TimeSlot,
                Status = data.Status
            };

            return View(await LoadDropdowns(vm));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(await LoadDropdowns(vm));

                await _service.UpdateAsync(new AppointmentDto
                {
                    AppointmentId = vm.AppointmentId,
                    PatientId = vm.PatientId,
                    DoctorId = vm.DoctorId,
                    Date = vm.Date,
                    TimeSlot = vm.TimeSlot,
                    Status = vm.Status
                });

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(await LoadDropdowns(vm));
            }
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null) return NotFound();

            return View(data);
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
        private async Task<AppointmentViewModel> LoadDropdowns(AppointmentViewModel vm)
        {
            var patients = await _patientService.GetAllAsync();
            var doctors = await _doctorService.GetAllAsync();

            vm.Patients = patients.Select(p => new SelectListItem
            {
                Value = p.PatientId.ToString(),
                Text = p.Name
            }).ToList();

            vm.Doctors = doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.Name
            }).ToList();

            return vm;
        }
    }
}
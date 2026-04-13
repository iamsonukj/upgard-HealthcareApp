using Microsoft.AspNetCore.Mvc;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.BLL.DTOs;

namespace HealthcareApp.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

      
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(DoctorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }

      
        [HttpPost]
        public async Task<IActionResult> Edit(DoctorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _service.GetByIdAsync(id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }

       
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
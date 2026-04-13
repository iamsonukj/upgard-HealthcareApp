using Microsoft.AspNetCore.Mvc;
using HealthcareApp.BLL.Interfaces;
using HealthcareApp.BLL.DTOs;

namespace HealthcareApp.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(string search)
        {
            var data = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(p =>
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    p.PhoneNumber.Contains(search)
                ).ToList();
            }

            ViewBag.Search = search;

            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        public async Task<IActionResult> Create(PatientDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.AddAsync(dto);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _service.GetByIdAsync(id);

            return View(data); 
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(PatientDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _service.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();

            return View(patient);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();

            return View(patient);
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
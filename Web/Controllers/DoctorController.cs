using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Services.Abstract;
using Web.Services.Concrete;
using Web.ViewModels.Doctor;
using Web.ViewModels.Faq;

namespace Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index(DoctorIndexVM model,string? name)
        {
            model = await _doctorService.GetAllDoctorAsync(model,name);
       
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.DoctorDetailsAsync(id);
            return View(doctor);
        }

        //[HttpGet]
        //public async Task<IActionResult> FilterByName(string name,DoctorIndexVM model)
        //{
        //    var doctor = await _doctorService.FilterByName(name);
        //    model = new DoctorIndexVM
        //    {
        //        Doctors = doctor
        //    };
        //    return PartialView("_DoctorPartial", model);
        //}


    }
}

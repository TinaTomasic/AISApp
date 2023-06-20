using Microsoft.AspNetCore.Mvc;
using AISApp.Data;
using AISApp.Data.Enum;
using AISApp.Models;
using Microsoft.EntityFrameworkCore;
using AISApp.Interfaces;
using AISApp.ViewModels;

namespace AISApp.Controllers
{
    public class HospitalController : Controller
    {
        
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HospitalController(IHospitalRepository hospitalRepository, IHttpContextAccessor httpContextAccessor)
        {
            _hospitalRepository = hospitalRepository;
            _httpContextAccessor = new HttpContextAccessor();
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Hospital> hospitals = await _hospitalRepository.GetAll();
            return View(hospitals);
        }
        //HttpGet(/{id}) ali vec imam u program kod MapController route, Detail mi je index
        public async Task<IActionResult> Detail(int id)
        {
            Hospital hospital = await _hospitalRepository.GetByIdAsync(id);
            return View(hospital);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createHospitalViewModel = new CreateHospitalViewModel { AppUserId = curUserId };
            return View(createHospitalViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHospitalViewModel hospitalVM)
        {
            if (ModelState.IsValid)
            {
                var hospital = new Hospital
                {
                    Title = hospitalVM.Title,
                    Description = hospitalVM.Description,
                    Image = hospitalVM.Image,
                    HospitalCategory = hospitalVM.HospitalCategory,
                    AppUserId = hospitalVM.AppUserId,
                    Address = new Address
                    {
                        Street = hospitalVM.Address.Street,
                        City = hospitalVM.Address.City,
                    }
                };
                _hospitalRepository.Add(hospital);
                return RedirectToAction("Index");
            }
            return View(hospitalVM);
        }

        public async Task<IActionResult> Edit (int id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync (id);
            if(hospital == null) return View("Error");
            var hospitalVM = new EditHospitalViewModel
            {
                Title = hospital.Title,
                Description = hospital.Description,
                AddressId = hospital.AddressId,
                Address = hospital.Address,
                URL = hospital.Image,
                HospitalCategory = hospital.HospitalCategory

            };
            return View(hospitalVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditHospitalViewModel hospitalVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit hospital");
                return View("Edit", hospitalVM);
            }

            var userHospital = await _hospitalRepository.GetByIdAsyncNoTracking(id);
            if (userHospital != null)
            { 

            var hospital = new Hospital
            {
                Id = id,
                Title = hospitalVM.Title,
                Description = hospitalVM.Description,
                Image = hospitalVM.Image,
                AddressId = hospitalVM.AddressId,
                Address = hospitalVM.Address,
            };
            _hospitalRepository.Update(hospital);
            return RedirectToAction("Index");
        }
        else
        {
                return View(hospitalVM);
        }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var hospitalDetails = await _hospitalRepository.GetByIdAsync(id);
            if (hospitalDetails == null)
            {
                return View("Error");
            }
            return View(hospitalDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            var hospitalDetails = await _hospitalRepository.GetByIdAsync(id);
            if (hospitalDetails == null)
            {
                return View("Error");
            }
            _hospitalRepository.Delete(hospitalDetails);
            return RedirectToAction("Index");
        }


    }
}

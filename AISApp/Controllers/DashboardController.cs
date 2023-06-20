using AISApp.Data;
using AISApp.Interfaces;
using AISApp.Models;
using AISApp.Repository;
using AISApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AISApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor) 
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Contact = editVM.Contact;
        }
        public async Task<IActionResult> Index()
        {
           var userHospitals = await _dashboardRepository.GetAllUserHospitals();
            var dashboardViewModel = new DashboardViewModel()
            {
                Hospitals = userHospitals
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Contact = user.Contact
            };
            return View(editUserViewModel);
        
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }
            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);
            MapUserEdit(user, editVM);

            _dashboardRepository.Update(user);

            return RedirectToAction("Index");

        }
    }
}

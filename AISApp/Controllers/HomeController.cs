using AISApp.Helpers;
using AISApp.Interfaces;
using AISApp.Models;
using AISApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace AISApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHospitalRepository _hospitalRepository;

        public HomeController(ILogger<HomeController> logger, IHospitalRepository hospitalRepository)
        {
            _logger = logger;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=e792164c183acf";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Hospitals = await _hospitalRepository.GetHospitalByCity(homeViewModel.City);
                }
                return View(homeViewModel);
            }
            catch (Exception)
            {
                homeViewModel.Hospitals = null;
            }

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
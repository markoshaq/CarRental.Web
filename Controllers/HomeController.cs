using CarRental.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CarRental.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        // CUSTOMER
        public IActionResult Customers()
        {
            return View();
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        // RENTAL CONTRACT
        public IActionResult RentalContracts()
        {
            return View();
        }

        public IActionResult AddRentalContract()
        {
            return View();
        }

        // VEHICLE
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VehicleList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7036/api/vehicles");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(json);
                return View(vehicles);
            }

            return View(new List<Vehicle>());
        }
        [HttpPost]
        public IActionResult SendAddVehicleForm(Vehicle vehicle)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://localhost:7036/api/vehicles", content);

            return RedirectToAction("VehicleList", "Home");
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
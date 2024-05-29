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
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7036/api/customers");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                return View(customers);
            }

            return View(new List<Customer>());
        }
        [HttpPost]
        public async Task<IActionResult> SendAddCustomerForm(Customer customer)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7036/api/customers", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7036/api/customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
            }

            return BadRequest();
        }

        // RENTAL CONTRACT

        public IActionResult AddRentalContract()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RentalContractList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7036/api/rentalcontracts");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rentalcontracts = JsonConvert.DeserializeObject<List<RentalContract>>(json);
                return View(rentalcontracts);
            }

            return View(new List<RentalContract>());
        }
        [HttpPost]
        public IActionResult SendAddRentalContractForm(RentalContract rentalcontract)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(rentalcontract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("https://localhost:7036/api/rentalcontracts", content);

            return RedirectToAction("RentalContractList", "Home");
        }

        public async Task<IActionResult> DeleteRentalContract(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7036/api/rentalcontract/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RentalContractList");
            }

            return BadRequest();
        }

        // VEHICLE
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveVehicle(Vehicle vehicle, bool isUpdate)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (isUpdate)
            {
                // Perform a PUT request to update the vehicle
                response = await client.PutAsync($"https://localhost:7036/api/vehicles/{vehicle.Id}", content);
            }
            else
            {
                // Perform a POST request to create a new vehicle
                response = await client.PostAsync("https://localhost:7036/api/vehicles", content);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("VehicleList");
            }
            else
            {
                return View("Error");
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditVehicle(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7036/api/vehicles/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);
                return View(vehicle); // Pass the vehicle data to the edit view
            }

            return NotFound();
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
        public async Task<IActionResult> SendAddVehicleForm(Vehicle vehicle)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(vehicle);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7036/api/vehicles", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("VehicleList", "Home");
            }
            else
            {
                // Handle error scenario
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7036/api/vehicles/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("VehicleList");
            }

            return BadRequest();
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
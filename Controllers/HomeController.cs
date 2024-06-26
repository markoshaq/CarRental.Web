﻿using CarRental.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CarRental.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        // HOMEPAGE
        public IActionResult Index()
        {
            return View();
        }

        // CUSTOMER
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
        public IActionResult AddCustomer()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7036/api/customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(json);
                return View(customer);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustomer(Customer customer, bool isUpdate)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (isUpdate)
            {
                response = await client.PutAsync($"https://localhost:7036/api/customers/{customer.Id}", content);
            }
            else
            {
                response = await client.PostAsync("https://localhost:7036/api/customers", content);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("CustomerList");
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

        public IActionResult AddRentalContract()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendAddRentalContractForm(RentalContract rentalcontract)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(rentalcontract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7036/api/rentalcontracts", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RentalContractList", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRentalContract(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7036/api/rentalcontracts/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rentalcontract = JsonConvert.DeserializeObject<RentalContract>(json);
                return View(rentalcontract);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRentalContract(RentalContract rentalcontract, bool isUpdate)
        {
            var client = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(rentalcontract);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (isUpdate)
            {
                response = await client.PutAsync($"https://localhost:7036/api/rentalcontracts/{rentalcontract.Id}", content);
            }
            else
            {
                response = await client.PostAsync("https://localhost:7036/api/rentalcontracts", content);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RentalContractList");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> DeleteRentalContract(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7036/api/rentalcontracts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("RentalContractList");
            }

            return BadRequest();
        }

        // VEHICLE
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

        public IActionResult AddVehicle()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> EditVehicle(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7036/api/vehicles/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var vehicle = JsonConvert.DeserializeObject<Vehicle>(json);
                return View(vehicle);
            }

            return NotFound();

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
                response = await client.PutAsync($"https://localhost:7036/api/vehicles/{vehicle.Id}", content);
            }
            else
            {
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

        // ERRORS AND MISC
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
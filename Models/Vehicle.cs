﻿namespace CarRental.Web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? LicensePlate { get; set; }
        public int? Class { get; set; }
        public decimal? Mileage { get; set; }
        public string? FirstRegistration { get; set; }
        public decimal? TirePressure { get; set; }
        public decimal? RentPrice { get; set; }
    }
}

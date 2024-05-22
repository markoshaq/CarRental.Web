using Newtonsoft.Json;

namespace CarRental.Web.Models
{
    public class Vehicle
    {
        public string Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("licensePlate")]
        public string LicensePlate { get; set; }

        [JsonProperty("class")]
        public int? Class { get; set; }

        [JsonProperty("mileage")]
        public decimal? Mileage { get; set; }

        [JsonProperty("firstRegistration")]
        public string? FirstRegistration { get; set; }

        [JsonProperty("tirePressure")]
        public decimal? TirePressure { get; set; }

        [JsonProperty("rentPrice")]
        public decimal RentPrice { get; set; }
    }
}

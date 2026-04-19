using System.ComponentModel.DataAnnotations;


namespace MapsCountryTest.Api.Models
{
    public class CoordinatesDto
    {
        /// <summary>
        /// DTO для хранения координат офиса.
        /// </summary>
        [Key]
        public int OfficeId { get; set; } // Идентификатор офиса.
        public double Latitude { get; set; } // Широта местоположения.
        public double Longitude { get; set; } // Долгота местоположения.
    }
}

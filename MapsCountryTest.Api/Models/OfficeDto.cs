using DataAccess.Context.Models;
using MapsCountryTest.Api.Models;

public class OfficeDto
{
    // Уникальный идентификатор офиса
    public int Id { get; set; }
    // Код офиса (уникальный код)
    public string? Code { get; set; }
    // Код города
    public int CityCode { get; set; }
    // Уникальный идентификатор (UUID)
    public string? Uuid { get; set; }
    // Тип офиса
    public OfficeType? Type { get; set; }
    // Код страны
    public string CountryCode { get; set; }
    // Координаты местоположения офиса
    public CoordinatesDto Coordinates { get; set; }
    // Регион адреса
    public string? AddressRegion { get; set; }
    // Город адреса
    public string? AddressCity { get; set; }
    // Улица адреса
    public string? AddressStreet { get; set; }
    // Номер дома
    public string? AddressHouseNumber { get; set; }
    // Номер квартиры
    public int? AddressApartment { get; set; }
    // Режим работы (например, "Пн-Пт, 9:00-18:00")
    public string WorkTime { get; set; }
    // Список телефонов офиса
    public List<PhoneDto> Phones { get; set; }
}

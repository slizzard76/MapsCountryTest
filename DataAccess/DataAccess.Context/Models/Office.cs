using DataAccess.Context.BaseEntity;
using DataAccess.Context.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Представляет модель данных для офиса.
/// Содержит всю необходимую информацию о местоположении, контактах и рабочем времени.
/// </summary>
[Index("AddressRegion", "AddressCity")]
public class Office : BaseEntity
{
    /// <summary>
    /// Код офиса.
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// Код города, к которому относится офис.
    /// </summary>
    public int CityCode { get; set; }
    /// <summary>
    /// Уникальный идентификатор офиса (UUID).
    /// </summary>
    public string? Uuid { get; set; }
    /// <summary>
    /// Тип офиса.
    /// </summary>
    public OfficeType? Type { get; set; }
    /// <summary>
    /// Код страны, к которой относится офис.
    /// </summary>
    public string CountryCode { get; set; }
    /// <summary>
    /// Координаты местоположения офиса.
    /// </summary>
    public virtual Coordinates Coordinates { get; set; }
    /// <summary>
    /// Регион адреса.
    /// </summary>
    public string? AddressRegion { get; set; }
    /// <summary>
    /// Город адреса.
    /// </summary>
    public string? AddressCity { get; set; }
    /// <summary>
    /// Улица адреса.
    /// </summary>
    public string? AddressStreet { get; set; }
    /// <summary>
    /// Номер дома адреса.
    /// </summary>
    public string? AddressHouseNumber { get; set; }
    /// <summary>
    /// Номер квартиры адреса.
    /// </summary>
    public int? AddressApartment { get; set; }
    /// <summary>
    /// Рабочее время офиса.
    /// </summary>
    public string WorkTime { get; set; }
    /// <summary>
    /// Список телефонов офиса.
    /// </summary>
    public virtual List<Phone> Phones { get; set; }
}

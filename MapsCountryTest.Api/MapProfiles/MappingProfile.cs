using AutoMapper;
using MapsCountryTest.Api.Models;

namespace MapsCountryTest.Api.MapProfiles;

/// <summary>
/// Конфигурация маппинга
/// </summary>
public class MappingProfile : Profile
{
    /// <inheritdoc />
    public MappingProfile()
    {
        CreateMap<Office, OfficeDto>();
        CreateMap<Phone, PhoneDto>();
        CreateMap<Coordinates, CoordinatesDto>();
    }   
}

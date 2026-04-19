using MediatR;

namespace MapsCountryTest.Api.Handlers.Query.Request
{
    /// <summary>
    /// Запрос на получение списка офисов.
    /// </summary>
    /// <typeparam name="TResponse">Тип возвращаемого списка офисов.</typeparam>
    public class GetOfficesRequest : IRequest<List<OfficeDto>>
    {
        /// <summary>
        /// Город, для которого запрашиваются офисы. Может быть null или пустым.
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Регион, для которого запрашиваются офисы. Может быть null или пустым.
        /// </summary>
        public string Region { get; set; }
    }   
}

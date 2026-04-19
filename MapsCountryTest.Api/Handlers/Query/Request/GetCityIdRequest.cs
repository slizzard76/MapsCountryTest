using MediatR;

namespace MapsCountryTest.Api.Handlers.Query.Request
{
    /// <summary>
    /// Запрос для получения идентификатора города по названию города и региона.
    /// </summary>
    public class GetCityIdRequest : IRequest<int>
    {
        /// <summary>
        /// Название города.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Регион, к которому относится город.
        /// </summary>
        public string Region { get; set; }
    }   
}

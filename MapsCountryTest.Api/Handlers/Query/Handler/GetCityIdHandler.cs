using MapsCountryTest.Api.Repository.Abstraction;
using MapsCountryTest.Api.Handlers.Query.Request;
using MediatR;

namespace MapsCountryTest.Api.Handlers.Query.Handler
{
    /// <summary>
    /// Обработчик запроса для получения идентификатора города (City ID).
    /// Реализует интерфейс IRequestHandler для обработки запроса GetCityIdRequest.
    /// </summary>
    public class GetCityIdHandler : IRequestHandler<GetCityIdRequest, int>
    {
        // Репозиторий для работы с данными офисов.
        private readonly IOfficeRepository _officeRepository;


        /// <summary>
        /// Конструктор GetCityIdHandler.
        /// </summary>
        /// <param name="officeRepository">Инстанс репозитория офисов.</param>
        public GetCityIdHandler(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }

        /// <summary>
        /// Асинхронно получает идентификатор города по заданным параметрам города и региона.
        /// </summary>
        /// <param name="request">Запрос, содержащий параметры города и региона.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Ожидаемая задача, возвращающая целочисленный идентификатор города.</returns>
        public async Task<int> Handle(GetCityIdRequest request, CancellationToken cancellationToken)
        {
            // Вызов репозитория для получения ID города.
            return await _officeRepository.GetCityIdAsync(request.City, request.Region, cancellationToken);
        }
    }   
}

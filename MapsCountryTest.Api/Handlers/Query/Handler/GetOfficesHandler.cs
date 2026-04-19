using AutoMapper;
using MapsCountryTest.Api.Repository.Abstraction;
using MapsCountryTest.Api.Handlers.Query.Request;
using MediatR;

namespace MapsCountryTest.Api.Handlers.Query.Handler
{
    /// <summary>
    /// Обработчик для получения списка офисов. Реализует интерфейс IRequestHandler.
    /// </summary>
    public class GetOfficesHandler : IRequestHandler<GetOfficesRequest, List<OfficeDto>>
    {
        // Поле для маппинга сущностей в DTO.
        private readonly IMapper _mapper;
        // Репозиторий для доступа к данным об офисах.
        private readonly IOfficeRepository _officeRepository;

        /// <summary>
        /// Конструктор класса. Инициализирует обработчик необходимыми зависимостями.
        /// </summary>
        /// <param name="mapper">Инстанс IMapper для преобразования объектов.</param>
        /// <param name="officeRepository">Инстанс IOfficeRepository для работы с данными.</param>
        public GetOfficesHandler(IMapper mapper, IOfficeRepository officeRepository)
        {
            _mapper = mapper;
            _officeRepository = officeRepository;
        }

        /// <summary>
        /// Обрабатывает запрос на получение списка офисов.
        /// </summary>
        /// <param name="request">Запрос, содержащий критерии поиска (город, регион).</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, возвращающая список DTO офисов.</returns>
        public async Task<List<OfficeDto>> Handle(GetOfficesRequest request, CancellationToken cancellationToken)
        {
            // Получаем список сущностей офисов из репозитория, используя критерии запроса.
            var offices = await _officeRepository.GetOfficesAsync(request.City, request.Region, cancellationToken);

            // Преобразуем список сущностей в список DTO с помощью маппера.
            var result = _mapper.Map<List<OfficeDto>>(offices);

            // Возвращаем преобразованный список.
            return result;

        }
    }
}

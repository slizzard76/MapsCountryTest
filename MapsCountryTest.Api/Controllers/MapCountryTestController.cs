using MapsCountryTest.Api.Handlers.Query.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MapsCountryTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapCountryTestController : ControllerBase
    {
        private readonly ILogger<MapCountryTestController> _logger;
        private readonly IMediator _mediator;

        public MapCountryTestController(ILogger<MapCountryTestController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Поиск терминалов города по названию города и области.
        /// Возвращает список офисов, найденных в указанном городе.
        /// </summary>
        /// <param name="cityName">Название города.</param>
        /// <param name="regionName">Название области.</param>
        /// <returns>Список найденных офисов.</returns>
        [HttpGet("search-terminals")]
        public async Task<ActionResult<List<OfficeDto>>> GetOfficesAsync(string cityName, string regionName, CancellationToken cancellationToken)
        {
            // Проверка наличия обязательных параметров: название города и региона.
            if (string.IsNullOrWhiteSpace(cityName) || string.IsNullOrWhiteSpace(regionName))
            {
                // Логирование предупреждения о попытке запроса без необходимых данных.
                _logger.LogWarning("Предпринята попытка поиска терминалов, у которых отсутствует название города или региона.");
                return BadRequest("Для поиска терминалов необходимо указать название города и региона.");
            }

            var offices = await _mediator.Send(new GetOfficesRequest
            {
                City = cityName,
                Region = regionName

            }, cancellationToken);

            if (offices == null || offices.Count == 0)
            {
                _logger.LogInformation("Терминалы (оффисы) для города: {City}, региона: {Region} не найдены.", cityName, regionName);
            }

            return Ok(offices);
        }

        /// <summary>
        /// Поиск идентификатора города по названию города и области
        /// </summary>
        /// <param name="cityName">Название города.</param>
        /// <param name="regionName">Название области/региона.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат поиска в виде списка объектов OfficeDto.</returns>
        [HttpGet("search-cityid")]
        public async Task<ActionResult<int>> GetCityIdAsync(string cityName, string regionName, CancellationToken cancellationToken)
        {
            // Проверка наличия обязательных параметров: название города и региона.
            if (string.IsNullOrWhiteSpace(cityName) || string.IsNullOrWhiteSpace(regionName))
            {
                // Логирование предупреждения о попытке запроса без необходимых данных.
                _logger.LogWarning("Предпринята попытка поиска идентификатора города, у которых отсутствует название города или региона.");
                return BadRequest("Для поиска идентификатора города необходимо указать название города и региона.");
            }

            var cityId = await _mediator.Send(new GetCityIdRequest
            { 
                City = cityName,
                Region = regionName
            } , cancellationToken);
            if (cityId == 0)
            {
                _logger.LogInformation("Идкнтификатор города {City}, региона: {Region} не найдены.", cityName, regionName);
                return NotFound();
            }

            return Ok(cityId);
        }
    }
}

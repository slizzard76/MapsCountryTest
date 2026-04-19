namespace MapsCountryTest.Api.Repository.Abstraction
{
    /// <summary>
    /// Репозиторий для работы с данными об офисах.
    /// Предоставляет методы для получения информации об офисах и идентификаторов городов.
    /// </summary>
    public interface IOfficeRepository
    {
        /// <summary>
        /// Асинхронно получает список всех офисов, соответствующих заданным критериям города и региона.
        /// </summary>
        /// <param name="city">Город, по которому нужно найти офисы.</param>
        /// <param name="region">Регион, по которому нужно найти офисы.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, содержащая коллекцию найденных объектов Office.</returns>
        Task<IEnumerable<Office>> GetOfficesAsync(string city, string region, CancellationToken cancellationToken);

        /// <summary>
        /// Асинхронно получает уникальный идентификатор города, соответствующего заданным критериям.
        /// </summary>
        /// <param name="city">Город для поиска ID.</param>
        /// <param name="region">Регион для поиска ID.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, содержащая целочисленный ID города.</returns>
        Task<int> GetCityIdAsync(string city, string region, CancellationToken cancellationToken);
    }   
}

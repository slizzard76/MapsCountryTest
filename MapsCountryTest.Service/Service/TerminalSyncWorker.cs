using MapsCountryTest.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;

namespace MapsCountryTest.Service
{
    public class TerminalSyncWorker
    {
        private readonly MapsCountryTestDbContext _dbContext;
        private readonly ILogger<TerminalSyncWorker> _logger;
        private readonly string _filePath;

        public TerminalSyncWorker(MapsCountryTestDbContext dbContext, ILogger<TerminalSyncWorker> logger, IConfiguration config)
        {
            _dbContext = dbContext;
            _logger = logger;
            _filePath = config["TerminalDataPath"] ?? throw new InvalidOperationException("File path not configured.");
        }

        public async Task LoadAndSaveTerminalsAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_filePath))
            {
                _logger.LogError("{Time} Файл справочника терминалов не найден по пути: {Path}", LogHelper.CurrentTime, _filePath);
                return;
            }

            try
            {
                var jsonString = await File.ReadAllTextAsync(_filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Обработка разных регистров
                    AllowTrailingCommas = true
                };

                var terminals = JsonSerializer.Deserialize<List<Office>>(jsonString, options);

                if (terminals == null || !terminals.Any())
                {
                    _logger.LogWarning("{Time} JSON файл пуст или не содержит корректных данных.", LogHelper.CurrentTime);
                    return;
                }
                _logger.LogInformation("{Time} Начинаем синхронизацию.", LogHelper.CurrentTime);
                var startTime = DateTime.Now;
                using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Snapshot, cancellationToken);

                // Очистка данных ---
                var oldCount = await _dbContext.Offices.CountAsync();
                _dbContext.Offices.RemoveRange(await _dbContext.Offices.ToListAsync()); // Удаление всех существующих записей
                await _dbContext.SaveChangesAsync();

                // Подготовка и Импорт данных (Bulk Insert)
                var newOffices = terminals;
                var newCount = newOffices.Count;

                _dbContext.Offices.AddRange(newOffices);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                // Логирование
                _logger.LogInformation("{Time} Время синхронизации {Seconds} секунд.", LogHelper.CurrentTime, (DateTime.Now - startTime).TotalSeconds);
                _logger.LogInformation("{Time} Цикл синхронизации завершен.", LogHelper.CurrentTime);
                _logger.LogInformation("{Time} Загружено {Count} терминалов из JSON", LogHelper.CurrentTime, newCount);
                _logger.LogInformation("{Time} Удалено {OldCount} старых записей", LogHelper.CurrentTime, oldCount);
                _logger.LogInformation("{Time} Сохранено {NewCount} новых терминалов", LogHelper.CurrentTime, newCount);

            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError("{Time} Критическая ошибка: Файл терминалов не найден. Ошибка: {ex}", LogHelper.CurrentTime, ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError("{Time} Ошибка десериализации JSON. Проверьте формат файла. Ошибка: {ex}", LogHelper.CurrentTime, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Time} Критическая ошибка при импорте данных.", LogHelper.CurrentTime, ex);
            }
        }
    }
}

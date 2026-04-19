using MapsCountryTest.Api.Helpers;

namespace MapsCountryTest.Service
{
    public class TerminalSyncBackgroundService : BackgroundService
    {
        private readonly ILogger<TerminalSyncBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _lifetime;
        private bool _shutdownRequested;

        private bool _isJustStarted;

        public TerminalSyncBackgroundService(ILogger<TerminalSyncBackgroundService> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _isJustStarted = true;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TimeSpan interval;
            _logger.LogInformation("{Time} Инициализация Background Service для синхронизации терминалов.", LogHelper.CurrentTime);
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                 
                    //Если нужна проверка при старте на 02:00 по МСК, иначе первый раз пройдет загрузка немедленно, и потому будем ждать до 2 часов

                    /*
                    if (_isJustStarted)
                    {
                        _isJustStarted = false;
                        _interval = LogHelper.TimeUntilNextStart();
                        _logger.LogInformation("{Time} Следующий запуск синхронизации через {hour} часов {minutes} минут {seconds} секунд.",
                            LogHelper.CurrentTime,
                            _interval.Hours,
                            _interval.Minutes,
                            _interval.Seconds);
                        await Task.Delay(_interval, stoppingToken);
                    }
                    */
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var syncService = scope.ServiceProvider.GetRequiredService<TerminalSyncWorker>();
                        await syncService.LoadAndSaveTerminalsAsync(stoppingToken);
                    }

                    interval = LogHelper.TimeUntilNextStart();
                    _logger.LogInformation("{Time} Следующий запуск синхронизации через {hour} часов {minutes} минут {seconds} секунд.",
                    LogHelper.CurrentTime,
                    interval.Hours,
                    interval.Minutes,
                    interval.Seconds);
                    //await Task.Delay(interval, stoppingToken);
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("{Time} Background Service остановлен.", LogHelper.CurrentTime);

            }
        }
    }
}

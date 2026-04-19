namespace MapsCountryTest.Api.Helpers
{
    /// <summary>
    /// Вспомогательный класс для логирования и работы с временными метками.
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Возвращает текущее время в формате "ГГГГ-ММ-ДД ЧЧ:ММ:СС".
        /// </summary>
        public static string CurrentTime =>  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// Вычисляет оставшееся время до следующего запланированного старта (02:00 по Москве).
        /// </summary>
        /// <returns>Продолжительность времени (TimeSpan) до следующего старта.</returns>
        public static TimeSpan TimeUntilNextStart()
        {
            TimeZoneInfo moscowTimeZone;
            try
            {
                // Поиск временной зоны "Europe/Moscow"
                moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            }
            catch (TimeZoneNotFoundException)
            {
                // Обработка случая, если временная зона не найдена в системе
                Console.WriteLine("Ошибка: В системе не найдена временная зона 'Europe/Moscow'. Проверьте ID.");
                return TimeSpan.Zero;
            }

            // Получение текущего времени в Москве (DateTimeOffset)
            DateTimeOffset nowMoscow = TimeZoneInfo.ConvertTime(DateTime.Now, moscowTimeZone);

            // Определяем целевое время сегодня (02:00:00)
            DateTime targetToday = nowMoscow.Date.AddHours(2);

            DateTime targetTime;

            // Проверка: если целевое время уже прошло сегодня, то ищем его завтра
            if (targetToday <= nowMoscow)
            {
                targetTime = targetToday.AddDays(1);
            }
            else
            {
                // Иначе, целевое время сегодня
                targetTime = targetToday;
            }
            
            // Вычисляем разницу во времени, используя UTC для точности
            TimeSpan remainingTime = targetTime.ToUniversalTime() - nowMoscow.ToUniversalTime();

            return remainingTime;
        }
    }   
}

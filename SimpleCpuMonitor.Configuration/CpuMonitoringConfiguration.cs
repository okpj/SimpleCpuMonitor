namespace SimpleCpuMonitor.Configuration
{
    /// <summary>
    /// Конфигурации мониторинга
    /// </summary>
    public class CpuMonitoringConfiguration
    {
        /// <summary>
        /// Максимальная загрузка
        /// </summary>
        public float MaxLoad { get; set; }

        /// <summary>
        /// Интервал мониторинга CPU в миллисекундах
        /// </summary>
        public int Interval { get; set; }
    }
}

namespace SimpleCpuMonitor.Configuration
{
    /// <summary>
    /// Конфигурации RabbitMQ
    /// </summary>
    public class RabbitMQConfigurations
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Адрес хоста
        /// </summary>
        public string Host { get; set; }


        public string Exchange { get; set; }

        public string Type { get; set; }

        public string RoutingKey { get; set; }
    }
}

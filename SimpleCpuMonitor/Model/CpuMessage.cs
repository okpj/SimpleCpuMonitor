using Newtonsoft.Json;

namespace SimpleCpuMonitor.Model
{

    public class CpuMessage
    {
        /// <summary>
        /// Время
        /// </summary>
        [JsonProperty("date_time")]
        public string DateTime { get; set; }

        /// <summary>
        /// Загрузка CPU
        /// </summary>
        [JsonProperty("usage")]
        public float Usage { get; set; }
    }
}

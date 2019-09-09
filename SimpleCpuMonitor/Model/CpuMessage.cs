using Newtonsoft.Json;
using System;

namespace SimpleCpuMonitor.Model
{

    public class CpuMessage
    {
        /// <summary>
        /// Время
        /// </summary>
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Загрузка CPU
        /// </summary>
        [JsonProperty("usage")]
        public float Usage { get; set; }
    }
}

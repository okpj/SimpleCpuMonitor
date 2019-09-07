using SimpleCpuMonitor.Helpers;
using System;
using System.Threading;

namespace SimpleCpuMonitor.Worker
{
    /// <summary>
    /// Мониторинг CPU
    /// </summary>
    public static class CpuMonitoring
    {
        #region Events
        /// <summary>
        /// Событие Получено состояние процессора
        /// </summary>
        public static event Action<float?> CPUStateReceivedEvent;

        /// <summary>
        /// Инициировать событие Получено состояние процессора
        /// </summary>
        public static void InvokeCPUStateReceived(float? info) => CPUStateReceivedEvent?.Invoke(info);

        /// <summary>
        /// Событие Произошла перегрузка процессора
        /// </summary>
        public static event Action<float> CPUOverloadEvent;

        /// <summary>
        /// Инициировать событие Произошла перегрузка процессора
        /// </summary>
        public static void InvokeCPUOverload(float value) => CPUOverloadEvent?.Invoke(value);

        #endregion

        /// <summary>
        /// Таймер
        /// </summary>
        private static Timer _timer;

        /// <summary>
        /// Запуск мониторинга
        /// </summary>
        /// <param name="maxUsage"></param>
        /// <param name="interval"></param>
        public static void StartCpuMonitoring(float maxUsage = 80.0F, int interval = 1000)
        {
            TimerCallback timerCallback = new TimerCallback(DataRequest);
            _timer = new Timer(timerCallback, maxUsage, 0, interval);
            Serilog.Log.Information("Мониторинг запущен...");
        }

        /// <summary>
        /// Остановка мониторинга
        /// </summary>
        public static void StopCpuMonitoring()
        {
            _timer?.Dispose();
            Serilog.Log.Information("Мониторинг остановлен...");
        }

        /// <summary>
        /// Получить данные
        /// </summary>
        /// <param name="maxUsage">Критичная загрузка процессора</param>
        public static void DataRequest(object maxUsage)
        {
            var totalLoad = CPUHelper.TotalLoad;
            InvokeCPUStateReceived(totalLoad);

            if (float.TryParse(maxUsage.ToString(), out float maxValue))
            {
                if (totalLoad.HasValue && totalLoad.Value > maxValue)
                {
                    InvokeCPUOverload(totalLoad.Value);
                }
            }
        }
    }
}

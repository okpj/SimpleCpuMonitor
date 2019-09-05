using SimpleCpuMonitor.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        private static CancellationTokenSource _monitoringTaskTokenSource;

        /// <summary>
        /// Запуск мониторинга CPU
        /// </summary>
        public static void StartCpuMonitoring(float? maxUsage = null)
        {
            try
            {
                Serilog.Log.Information("Мониторинг запущен...");
                _monitoringTaskTokenSource = new CancellationTokenSource();
                CancellationToken token = _monitoringTaskTokenSource.Token;
                StartMonitoringTask(maxUsage, token);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "StartCpuMonitoring");
                _monitoringTaskTokenSource.Dispose();
            }
        }

        /// <summary>
        /// Запуск задачи мониторинга
        /// </summary>
        /// <param name="maxUsage">Критичная нагрузка</param>
        /// <param name="token">Токен</param>
        private static void StartMonitoringTask(float? maxUsage, CancellationToken token)
        {
            Task.Run(() => MonitoringProcess(maxUsage, token), token)
                .ContinueWith(result => TaskСompletion(result));
        }

        /// <summary>
        /// Логика мониторинга
        /// </summary>
        /// <param name="maxUsage">Критичная нагрузка</param>
        /// <param name="token">Токен</param>
        private static void MonitoringProcess(float? maxUsage, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var totalLoad = CPUHelper.TotalLoad;
                InvokeCPUStateReceived(totalLoad);
                if (maxUsage.HasValue && totalLoad.HasValue && totalLoad.Value > maxUsage)
                {
                    InvokeCPUOverload(totalLoad.Value);
                }

                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Завершение таска
        /// </summary>
        /// <param name="result"></param>
        private static void TaskСompletion(Task result)
        {
            if (result.IsFaulted)
            {
                if (result.Exception != null)
                {
                    Serilog.Log.Error(result.Exception, "StartMonitoringTask");
                }
                _monitoringTaskTokenSource.Dispose();
            }
        }

        /// <summary>
        /// Завершить мониторинг
        /// </summary>
        public static void StopCpuMonitoring()
        {
            _monitoringTaskTokenSource.Cancel();
            Serilog.Log.Information("Мониторинг остановлен...");
        }

    }
}

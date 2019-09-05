using SimpleCpuMonitor.Helpers;

namespace SimpleCpuMonitor.Worker
{
    /// <summary>
    /// Генератор снимков нагрузки CPU
    /// </summary>
    public static class SnapshotGenerator
    {
        public static int FontSize { get; set; } = 24;
        public static string Font { get; set; } = "Arial";

        private static byte[] _imageByte = null;
        public static byte[] CPUSnapShot => _imageByte;

        /// <summary>
        /// Запустить генератор снимков
        /// </summary>
        public static void StartImageStream()
        {
            CpuMonitoring.CPUStateReceivedEvent += FillInCurrentPicture;
        }

        /// <summary>
        /// Остановить генератор снимков
        /// </summary>
        public static void StopImageStream()
        {
            CpuMonitoring.CPUStateReceivedEvent -= FillInCurrentPicture;
        }

        /// <summary>
        /// Создать картинку для переданного значения
        /// </summary>
        /// <param name="load"></param>
        private static void FillInCurrentPicture(float? load)
        {
            _imageByte = ImageHelper.TextToImage(load?.ToString() ?? "", Font, FontSize);
        }
    }
}

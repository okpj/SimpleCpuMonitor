using SimpleCpuMonitor.Configuration;
using SimpleCpuMonitor.Container;
using SimpleCpuMonitor.Helpers;

namespace SimpleCpuMonitor.Worker
{
    /// <summary>
    /// Генератор снимков нагрузки CPU
    /// </summary>
    public static class SnapshotGenerator
    {
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
            var font = CompositionRoot.Container.GetInstance<SnapshotConfiguration>().Font;
            var fontSize = CompositionRoot.Container.GetInstance<SnapshotConfiguration>().FontSize;
            _imageByte = ImageHelper.TextToImage(load?.ToString() ?? "", font, fontSize);
        }
    }
}

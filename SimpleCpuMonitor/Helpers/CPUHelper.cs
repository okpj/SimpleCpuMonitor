using HardwareProviders.CPU;
using System.Linq;

namespace SimpleCpuMonitor.Helpers
{
    public static class CPUHelper
    {
        /// <summary>
        /// Загрузка процессора
        /// </summary>
        public static float? TotalLoad => Cpu.Discover().FirstOrDefault()?.TotalLoad.Value;
    }
}

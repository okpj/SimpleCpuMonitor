using System.Drawing;
using System.IO;
using System.Linq;
using HardwareProviders.CPU;
using SimpleCpuMonitor.Helpers;

namespace SimpleCpuMonitor.Services
{
    /// <summary>
    /// Сервис CPU
    /// </summary>
    public class CPUService : ICPUService
    {
        public CPUService()
        { 

        }

        public float? GetTotalLoad() => CPUHelper.TotalLoad;

        public byte[] GetSnapshotCPULoad()
        {
            var load = CPUHelper.TotalLoad;
            var image = ImageHelper.TextToImage(load.ToString(), "Arial", 15);
            return image;
        }

        
    }
}

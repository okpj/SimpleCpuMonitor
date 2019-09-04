using System.Drawing;

namespace SimpleCpuMonitor.Services
{
    public interface ICPUService
    {
        float? GetTotalLoad();
        byte[] GetSnapshotCPULoad();
    }
}

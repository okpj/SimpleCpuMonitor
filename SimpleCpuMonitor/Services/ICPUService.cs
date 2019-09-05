namespace SimpleCpuMonitor.Services
{
    public interface ICPUService
    {
        /// <summary>
        /// Получить загрузку процессора
        /// </summary>
        /// <returns></returns>
        float? GetTotalLoad();
        /// <summary>
        /// Получить снимок загрузки процессора
        /// </summary>
        /// <returns></returns>
        byte[] GetSnapshotCPULoad();
    }
}

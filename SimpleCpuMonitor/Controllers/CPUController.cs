using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCpuMonitor.Services;

namespace SimpleCpuMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPUController : ControllerBase
    {
        private ICPUService _cpuService;
        public CPUController(ICPUService cpuService)
        {
            _cpuService = cpuService;
        }
      

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var image = _cpuService.GetSnapshotCPULoad();
            return File(image, "image/jpeg");
        }

    }
}

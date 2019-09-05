using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Threading;
using SimpleCpuMonitor.Worker;

namespace SimpleCpuMonitor.Controllers
{
    /// <summary>
    /// CPU контроллер
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CPUController : ControllerBase
    {
        private string _contentTypeForMJpeg = "multipart/x-mixed-replace; boundary=--separator";
        public CPUController() { }

        /// <summary>
        /// Получить MJPEG
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMjpeg")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public ActionResult GetMjpeg()
        {
            SnapshotGenerator.StartImageStream();
            Response.Clear();
            Response.ContentType = _contentTypeForMJpeg;
            var encoding = new ASCIIEncoding();
            var outputStream = new MemoryStream();
            while (!Response.HttpContext.RequestAborted.IsCancellationRequested)
            {
                try
                {
                    var buf = SnapshotGenerator.CPUSnapShot;
                    if (buf != null)
                    {
                        var boundaryString = new StringBuilder($"\r\n--separator\r\nContent-Type: image/jpeg\r\nContent-Length:{buf.Length}\r\n\r\n");
                        var boundary = encoding.GetBytes(boundaryString.ToString());
                        outputStream.Write(boundary, 0, boundary.Length);
                        outputStream.Write(buf, 0, buf.Length);
                        outputStream.Position = 0;
                        outputStream.CopyTo(Response.Body);
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Serilog.Log.Error(ex, "GetMjpeg");
                }
            }

            SnapshotGenerator.StopImageStream();
            return null;
        }
    }
}

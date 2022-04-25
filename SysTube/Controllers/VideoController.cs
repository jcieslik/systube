using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Data.Enums;
using Services.DTOs;
using Services.Models;

namespace SysTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpGet]
        [Route("GetVideosPaginated")] 
        public async Task<ActionResult<PaginatedList<VideoDTO>>> GetVideosPaginated([FromQuery] PaginationProperties paginationProperties)
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetFileById")]
        public async Task GetFileById(int fileId, Resolution resolution)
        {
            string filePath = videoService.GetVideoPathByIdAndResolution(fileId, Resolution._360p);

            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            var dataToRead = stream.Length;

            Response.Headers["Accept-Ranges"] = "bytes";
            Response.ContentType = "video/mp4";

            var buffer = new byte[4096];

            if (!string.IsNullOrEmpty(Request.Headers["Range"]))
            {
                string[] range = Request.Headers["Range"][0].Split(new char[] { '=', '-' });
                var startbyte = int.Parse(range[1]);
                var endByte = Math.Min(startbyte + 1000000, dataToRead - 1);
                stream.Seek(startbyte, SeekOrigin.Begin);

                Response.StatusCode = 206;
                Response.Headers["Content-Range"] = string.Format(" bytes {0}-{1}/{2}", startbyte, endByte, dataToRead);
                Response.Headers["Content-Length"] = (endByte - startbyte + 1).ToString(); 
            }

            Stream outputStream = Response.Body;
            var bytesToRead = 1000001;

            while (true)
            {
                if (bytesToRead > 0)
                {
                    var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, Math.Min(4096, bytesToRead)));
                    bytesToRead -= bytesRead;
                    await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                    if (bytesRead == 0)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            await outputStream.FlushAsync();
        }

        [HttpGet]
        [Route("GetFileById2")]
        public async Task GetFileById2(int fileId)
        {
            string filePath = videoService.GetVideoPathByIdAndResolution(fileId, Resolution._360p);

            Stream stream = new FileStream("C:/Users/jcies/Downloads/sample_640x360_surfing_with_audio.mp4", FileMode.Open, FileAccess.Read, FileShare.Read);

            var dataToRead = stream.Length;

            Response.Headers["Accept-Ranges"] = "bytes";
            Response.ContentType = "video/mp4";

            var buffer = new byte[4096];

            if (!string.IsNullOrEmpty(Request.Headers["Range"]))
            {
                string[] range = Request.Headers["Range"][0].Split(new char[] { '=', '-' });
                var startbyte = int.Parse(range[1]);
                var endByte = Math.Min(startbyte + 1000000, dataToRead - 1);
                stream.Seek(startbyte, SeekOrigin.Begin);

                Response.StatusCode = 206;
                Response.Headers["Content-Range"] = string.Format(" bytes {0}-{1}/{2}", startbyte, endByte, dataToRead);
                Response.Headers["Content-Length"] = (endByte - startbyte + 1).ToString();
            }

            Stream outputStream = Response.Body;
            var bytesToRead = 1000001;

            while (true)
            {
                if (bytesToRead > 0)
                {
                    var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, Math.Min(4096, bytesToRead)));
                    bytesToRead -= bytesRead;
                    await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                    if (bytesRead == 0)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            await outputStream.FlushAsync();
        }
    }
}

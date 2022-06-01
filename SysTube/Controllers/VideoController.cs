using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Data.Enums;
using Services.DTOs;
using Services.Models;
using Services.Exceptions;
using SysTube.Settings;
using Microsoft.Extensions.Options;

namespace SysTube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService videoService;
        private readonly IOptions<SettingsModel> settings;

        public VideoController(IVideoService videoService, IOptions<SettingsModel> settings)
        {
            this.videoService = videoService;
            this.settings = settings;
        }

        [HttpGet]
        [Route("GetVideoById")]
        public async Task<ActionResult<VideoDTO>> GetVideoById([FromQuery] int videoId)
        {
            var video = await videoService.GetVideoById(videoId);

            return Ok(video);
        }

        [HttpGet]
        [Route("GetVideosPaginated")] 
        public async Task<ActionResult<PaginatedList<VideoDTO>>> GetVideosPaginated([FromQuery] PaginationProperties paginationProperties, [FromQuery] string searchString) 
        {
            var videos = await videoService.GetVideosPaginated(paginationProperties, settings.Value.ThumbnailsPath, searchString);

            return Ok(videos);
        }

        [HttpGet]
        [Route("GetFileById")]
        public async Task<ActionResult> GetFileById(int fileId, Resolution resolution)
        {
            try
            {
                var videoWithFilepath = await videoService.GetVideoByIdAndResolution(fileId, resolution);

                Stream stream = new FileStream(settings.Value.VideosPath + "\\" + videoWithFilepath.Filepath, FileMode.Open, FileAccess.Read, FileShare.Read);

                var streamLength = (stream.Length / videoWithFilepath.SecondsLength) * 10;

                var dataToRead = stream.Length;

                Response.Headers["Accept-Ranges"] = "bytes";
                Response.ContentType = "video/mp4";

                var buffer = new byte[4096];

                if (!string.IsNullOrEmpty(Request.Headers["Range"]))
                {
                    string[] range = Request.Headers["Range"][0].Split(new char[] { '=', '-' });
                    var startbyte = int.Parse(range[1]);
                    var endByte = Math.Min(startbyte + streamLength, dataToRead - 1);
                    stream.Seek(startbyte, SeekOrigin.Begin);

                    Response.StatusCode = 206;
                    Response.Headers["Content-Range"] = string.Format(" bytes {0}-{1}/{2}", startbyte, endByte, dataToRead);
                    Response.Headers["Content-Length"] = (endByte - startbyte + 1).ToString();
                }

                Stream outputStream = Response.Body;
                var bytesToRead = streamLength;

                while (true)
                {
                    if (bytesToRead > 0)
                    {
                        var bytesRead = await stream.ReadAsync(buffer.AsMemory(0, Math.Min(4096, (int)bytesToRead)));
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

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddVideo")]
        public async Task<ActionResult> AddVideo(CreateVideoDTO newVideo)
        {
            var video = await videoService.AddVideo(newVideo, settings.Value.ThumbnailsPath);

            return Ok(video);
        }

        [HttpPost]
        [Route("AddVideoFile")]
        public async Task<ActionResult> AddVideoFile(CreateVideoFileDTO newVideoFile)
        {
            try
            {
                var video = await videoService.AddVideoFile(newVideoFile, settings.Value.ThumbnailsPath);

                return Ok(video);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetVideosForSidebar")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideosForSidebar(int currentVideoId)
        {
            var videos = await videoService.GetVideosForSidebar(currentVideoId, settings.Value.ThumbnailsPath);

            return Ok(videos);
        }

        [HttpPut]
        [Route("IncrementWatched")]
        public async Task<ActionResult> IncrementWatched(int videoId)
        {
            await videoService.IncrementWatchedCounter(videoId);

            return Ok();
        }
    }
}

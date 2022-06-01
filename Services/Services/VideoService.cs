using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Exceptions;
using Services.Extensions;
using Services.Interfaces;
using Services.Models;

namespace Services.Services
{
    public class VideoService : IVideoService
    {
        private readonly IDatabaseContext context;

        public VideoService(IDatabaseContext context) 
        {
            this.context = context;
        }

        public async Task<VideoWithFilepathDTO> GetVideoByIdAndResolution(int fileId, Resolution resolution)
        {
            var video = await context.Videos
                .Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.Id == fileId);

            if (video == null)
            {
                throw new NotFoundException(nameof(Video), fileId);
            }

            var file = video.Files.FirstOrDefault(x => x.Resolution == resolution);

            if (file == null)
            {
                throw new NotFoundException(nameof(Video), fileId);
            }

            var videoWithFile = new VideoWithFilepathDTO(video, video.ThumbnailFilepath, file.Path);

            return videoWithFile;
        }

        public async Task<PaginatedList<VideoDTO>> GetVideosPaginated(PaginationProperties paginationProperties, string thumbnailsPath, string searchString = "")
        {
            var searchSubstrings = searchString.Split(" ");

            var videos = await context.Videos
                .Include(x => x.Files)
                .Where(x => searchSubstrings.Any(x.Title.Contains))
                .AsQueryable()
                .ToPaginatedListAsync(paginationProperties.PageIndex, paginationProperties.PageSize);

            return new PaginatedList<VideoDTO>(videos.Items.Select(x => new VideoDTO(x, thumbnailsPath)).ToList(), videos.Items.Count, videos.PageIndex, videos.TotalCount);
        }

        public async Task<VideoDTO> AddVideo(CreateVideoDTO video, string thumbnailsPath)
        {
            var entity = new Video
            {
                Description = video.Description,
                Title = video.Title,
                SecondsLength = video.SecondsLength,
                WatchedCounter = 0,
                ThumbnailFilepath = video.ThumbnailFilepath,
                Files = new List<VideoFile>()
            };

            context.Videos.Add(entity);

            await context.SaveChangesAsync();

            return new VideoDTO(entity, thumbnailsPath);
        }

        public async Task<VideoDTO> AddVideoFile(CreateVideoFileDTO file, string thumbnailsPath)
        {
            var video = await context.Videos.FindAsync(file.VideoId);

            if (video == null)
            {
                throw new NotFoundException(nameof(Video), file.VideoId);

            }

            var entity = new VideoFile
            {
                Resolution = file.Resolution,
                Path = file.Path,
                Video = video
            };

            context.VideoFiles.Add(entity);

            await context.SaveChangesAsync();

            video = await context.Videos.FindAsync(file.VideoId);

            return new VideoDTO(video, thumbnailsPath);
        }

        public async Task<IEnumerable<VideoDTO>> GetVideosForSidebar(int currentVideoId, string thumbnailPath)
        {
            return await context.Videos
                .Include(x => x.Files)
                .Where(x => x.Id != currentVideoId)
                .OrderBy(arg => Guid.NewGuid())
                .Take(5)
                .Select(x => new VideoDTO(x, thumbnailPath))
                .ToListAsync();
        }

        public async Task IncrementWatchedCounter(int videoId)
        {
            var video = await context.Videos.FindAsync(videoId);

            if (video == null)
            {
                throw new NotFoundException(nameof(Video), videoId);
            }

            video.WatchedCounter++;

            await context.SaveChangesAsync();
        }

        public async Task<VideoDTO> GetVideoById(int videoId)
        {
            var video = await context.Videos.Include(x => x.Files).FirstOrDefaultAsync(x => x.Id == videoId);

            if (video == null)
            {
                throw new NotFoundException(nameof(Video), videoId);
            }

            return new VideoDTO(video);
        }
    }
}

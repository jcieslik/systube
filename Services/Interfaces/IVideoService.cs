﻿using Data.Enums;
using Services.DTOs;
using Services.Models;

namespace Services.Interfaces
{
    public interface IVideoService
    {
        Task<VideoWithFilepathDTO> GetVideoByIdAndResolution(int fileId, Resolution resolution);

        PaginatedList<VideoDTO> GetVideosPaginated(PaginationProperties paginationProperties, string thumbnailPath, string searchString);

        Task<IEnumerable<VideoDTO>> GetVideosForSidebar(int currentVideoId, string thumbnailPath);

        Task<VideoDTO> AddVideo(CreateVideoDTO video, string thumbnailsPath);

        Task<VideoDTO> AddVideoFile(CreateVideoFileDTO file, string thumbnailsPath);

        Task IncrementWatchedCounter(int videoId);

        Task<VideoDTO> GetVideoById(int videoId);
    }
}
using Data.Entities;

namespace Services.DTOs
{
    public class VideoDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailFilepath { get; set; }

        public long WatchedCounter { get; set; }

        public long SecondsLength { get; set; }

        public byte[] Thumbnail { get; set; }

        public VideoDTO(Video video, byte[] thumbnail)
        {
            Id = video.Id;
            Title = video.Title;
            Description = video.Description;
            ThumbnailFilepath = video.ThumbnailFilepath;
            WatchedCounter = video.WatchedCounter;
            SecondsLength = video.SecondsLength;
            Thumbnail = thumbnail;
        }
    }
}

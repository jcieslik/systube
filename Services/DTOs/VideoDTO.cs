using Data.Entities;
using Data.Enums;

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

        public IEnumerable<Resolution> AvailableResolutions { get; set; }

        public VideoDTO(Video video, string thumbnailPath)
        {
            Id = video.Id;
            Title = video.Title;
            Description = video.Description;
            ThumbnailFilepath = video.ThumbnailFilepath;
            WatchedCounter = video.WatchedCounter;
            SecondsLength = video.SecondsLength;
            AvailableResolutions = video.Files.Select(x => x.Resolution).Distinct();

            try
            {
                var fileInfo = new FileInfo(thumbnailPath + video.ThumbnailFilepath);
                Thumbnail = new byte[fileInfo.Length];
                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(Thumbnail, 0, Thumbnail.Length);
                }
            }
            catch (Exception)
            {
                Thumbnail = Array.Empty<byte>();
            }
        }

        public VideoDTO(Video video)
        {
            Id = video.Id;
            Title = video.Title;
            Description = video.Description;
            WatchedCounter = video.WatchedCounter;
            SecondsLength = video.SecondsLength;
            AvailableResolutions = video.Files.Select(x => x.Resolution).Distinct();
        }
    }
}

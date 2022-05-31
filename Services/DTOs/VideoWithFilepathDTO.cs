using Data.Entities;

namespace Services.DTOs
{
    public class VideoWithFilepathDTO : VideoDTO
    {
        public string Filepath { get; set; }

        public VideoWithFilepathDTO(Video video, string thumbnailPath, string filepath) : base(video, thumbnailPath)
        {
            Filepath = filepath;
        }
    }
}

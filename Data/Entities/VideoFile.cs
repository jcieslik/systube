using Data.Enums;

namespace Data.Entities
{
    public class VideoFile : Entity
    {
        public string Path { get; set; }

        public Resolution Resolution { get; set; }

        public Video Video { get; set; }

        public int VideoId { get; set; }
    }
}

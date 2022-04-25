using Data.Enums;

namespace Services.DTOs
{
    public class CreateVideoFileDTO
    {
        public int VideoId { get; set; }

        public string Path { get; set; }

        public Resolution Resolution { get; set; }
    }
}

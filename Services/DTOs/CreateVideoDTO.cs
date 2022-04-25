namespace Services.DTOs
{
    public class CreateVideoDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailFilepath { get; set; }

        public long SecondsLength { get; set; }
    }
}

namespace Data.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<VideoFile> VideoFiles { get; set; }
    }
}

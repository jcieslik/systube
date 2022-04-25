namespace Data.Entities
{
    public class Video : Entity
    {
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string ThumbnailFilepath { get; set; }

        public long WatchedCounter { get; set; }

        public long SecondsLength { get; set; }
        
        public ICollection<Video> Videos { get; set; }
    }
}

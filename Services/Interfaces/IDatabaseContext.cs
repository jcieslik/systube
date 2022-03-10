using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services.Interfaces
{
    public interface IDatabaseContext
    {
        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoFile> VideoFiles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

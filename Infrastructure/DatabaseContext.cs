using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Infrastructure.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoFile> VideoFiles { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}

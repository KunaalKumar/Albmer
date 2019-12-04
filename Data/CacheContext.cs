using Microsoft.EntityFrameworkCore;

namespace Albmer.Models
{
    public class CacheContext : DbContext
    {
        public CacheContext(DbContextOptions<CacheContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistAlbum>()
                .HasKey(rel => new { rel.ArtistId, rel.AlbumId });

            // Artist to album relation
            modelBuilder.Entity<ArtistAlbum>()
                .HasOne(rel => rel.Artist)
                .WithMany(artist => artist.Albums)
                .HasForeignKey(rel => rel.ArtistId);

            // Album to artist relation
            modelBuilder.Entity<ArtistAlbum>()
                .HasOne(rel => rel.Album)
                .WithMany(album => album.Artists)
                .HasForeignKey(rel => rel.AlbumId);
        }
    }
}

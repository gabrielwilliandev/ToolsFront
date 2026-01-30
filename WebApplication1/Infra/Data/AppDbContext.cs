using Azure;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToolsEntitie> Tools { get; set; }
        public DbSet<TagEntitie> Tags { get; set; }
        public DbSet<ToolTag> ToolTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToolsEntitie>()
                .HasKey(t => t.ToolId);

            modelBuilder.Entity<TagEntitie>()
                .HasKey(t => t.TagId);

            modelBuilder.Entity<ToolTag>()
                .HasKey(tt => new { tt.ToolId, tt.TagId });

            modelBuilder.Entity<ToolTag>()
                .HasOne(tt => tt.Tool)
                .WithMany(t => t.ToolTags)
                .HasForeignKey(tt => tt.ToolId);

            modelBuilder.Entity<ToolTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.ToolTags)
                .HasForeignKey(tt => tt.TagId);

            modelBuilder.Entity<TagEntitie>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}

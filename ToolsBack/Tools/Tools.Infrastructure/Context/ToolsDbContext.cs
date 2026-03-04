using Microsoft.EntityFrameworkCore;
using Tools.Domain.Entities;

namespace Tools.Infrastructure.Context
{
    public class ToolsDbContext : DbContext
    {
        public ToolsDbContext(DbContextOptions<ToolsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tool>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tools)
                .UsingEntity(j => j.ToTable("ToolTags"));
        }

        public DbSet<Tool> Tools { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}

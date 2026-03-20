using Microsoft.EntityFrameworkCore;
using Tools.Domain.Entities;

namespace Tools.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tool>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tools)
                .UsingEntity(j => j.ToTable("ToolTags"));

            modelBuilder.Entity<Tag>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

          
            modelBuilder.Entity<Contact>()
                .Property(x => x.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Contact>()
                .Property(x => x.Status)
                .HasConversion<string>();
        
        }

        public DbSet<Tool> Tools { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}

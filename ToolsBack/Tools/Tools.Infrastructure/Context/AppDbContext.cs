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
                .HasOne(t => t.Lista)
                .WithMany(l => l.Tools)
                .HasForeignKey(t => t.ListaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

          
            modelBuilder.Entity<Contact>()
                .Property(x => x.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Contact>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Lista>()
                .HasOne(l => l.User)
                .WithMany(u => u.Listas)
                .HasForeignKey(l => l.UserId);
        }

        public DbSet<Tool> Tools { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lista> Listas { get; set; }
    }
}

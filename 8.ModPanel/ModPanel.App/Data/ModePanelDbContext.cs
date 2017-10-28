using Microsoft.EntityFrameworkCore;
using ModPanel.App.Data.Models;

namespace ModPanel.App.Data
{
    public class ModePanelDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=ModPanelExamDb;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Post)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}

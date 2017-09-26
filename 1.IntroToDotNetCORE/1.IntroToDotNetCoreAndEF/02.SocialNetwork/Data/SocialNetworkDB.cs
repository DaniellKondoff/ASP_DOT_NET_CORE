using _02.SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _02.SocialNetwork.Data
{
    public class SocialNetworkDB : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=SocialNetworkCore;Integrated Security=True;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserFriend>()
                 .HasKey(uf => new { uf.UserId, uf.FriendId });

            builder.Entity<UserFriend>()
                .HasOne(x => x.User)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFriend>()
                .HasOne(x => x.Friend)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Albums)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.Entity<AlbumsPictures>()
                .HasKey(ap => new { ap.AlbumId, ap.PictureId });

            builder.Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Album)
                .HasForeignKey(p => p.AlbumId);

            builder.Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithOne(a => a.Picture)
                .HasForeignKey(a => a.PictureId);

            base.OnModelCreating(builder);
        }
    }
}

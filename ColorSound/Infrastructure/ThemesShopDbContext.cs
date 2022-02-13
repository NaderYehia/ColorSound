using Grad_Proj.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Infrastructure
{
    public class ThemesShopDbContext:DbContext
    {
        public ThemesShopDbContext(DbContextOptions<ThemesShopDbContext> ops)
           : base(ops)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorToAuthor> AuthorToAuthor { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImages> ItemImages { get; set; }
        public DbSet<ItemLikes> ItemLikes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorToAuthor>()
                .HasKey(k => new { k.AuthorId, k.FollowerId });

            modelBuilder.Entity<AuthorToAuthor>()
                .HasOne(l => l.Author)
                .WithMany(a => a.Followers)
                .HasForeignKey(l => l.AuthorId);

            modelBuilder.Entity<AuthorToAuthor>()
                .HasOne(l => l.Follower)
                .WithMany(a => a.Following)
                .HasForeignKey(l => l.FollowerId);
        }
    }
}

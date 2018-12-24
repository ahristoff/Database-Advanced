using Forum.Data.Models;
using Lab.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
        {
        }
        public ForumDbContext( DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostsTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany(c => c.Posts)             
                .WithOne(c => c.Category)          
                .HasForeignKey(c => c.CategoryId); 

            builder.Entity<Post>()
                .HasMany(c => c.Replies)            
                .WithOne(c => c.Post)              
                .HasForeignKey(c => c.PostId)       
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(c => c.Posts)
                .WithOne(c => c.Author)            
                .HasForeignKey(c => c.AuthorId);

            builder.Entity<User>()
                .HasMany(c => c.Replies)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            //builder.Entity<User>()
            //        .Property(p => p.Username)
            //        .HasColumnType("Author");
                    

            //----------------------------------------Many to Many
            builder.Entity<PostTag>()                
                .HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostsTags)
                .HasForeignKey(c => c.PostId);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(p => p.PostsTags)
                .HasForeignKey(c => c.TagId);
        }
    }
}

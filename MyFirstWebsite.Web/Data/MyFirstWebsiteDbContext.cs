using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Data
{
    public class MyFirstWebsiteDbContext : DbContext
    {
        public MyFirstWebsiteDbContext(DbContextOptions<MyFirstWebsiteDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}

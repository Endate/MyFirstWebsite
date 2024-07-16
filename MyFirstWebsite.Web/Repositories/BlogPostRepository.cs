using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Data;
using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly MyFirstWebsiteDbContext myFirstWebsiteDbContext;

        public BlogPostRepository(MyFirstWebsiteDbContext myFirstWebsiteDbContext)
        {
            this.myFirstWebsiteDbContext = myFirstWebsiteDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await myFirstWebsiteDbContext.AddAsync(blogPost);
            await myFirstWebsiteDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await myFirstWebsiteDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                myFirstWebsiteDbContext.BlogPosts.Remove(existingBlog);
                await myFirstWebsiteDbContext.SaveChangesAsync();

                return existingBlog;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await myFirstWebsiteDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await myFirstWebsiteDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await myFirstWebsiteDbContext.BlogPosts.Include(x => x.Tags).
                FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await myFirstWebsiteDbContext.BlogPosts.Include(x => x.Tags)
                 .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null) 
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.Author = blogPost.Author;
                existingBlog.Content = blogPost.Content;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await myFirstWebsiteDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }


    }
}

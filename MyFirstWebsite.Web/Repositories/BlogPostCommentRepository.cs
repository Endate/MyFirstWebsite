using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Data;
using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly MyFirstWebsiteDbContext myFirstWebsiteDbContext;

        public BlogPostCommentRepository(MyFirstWebsiteDbContext myFirstWebsiteDbContext)
        {
            this.myFirstWebsiteDbContext = myFirstWebsiteDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await myFirstWebsiteDbContext.BlogPostComment.AddAsync(blogPostComment);
            await myFirstWebsiteDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await myFirstWebsiteDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}

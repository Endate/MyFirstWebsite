using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Data;
using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly MyFirstWebsiteDbContext myFirstWebsiteDbContext;

        public BlogPostLikeRepository(MyFirstWebsiteDbContext myFirstWebsiteDbContext)
        {
            this.myFirstWebsiteDbContext = myFirstWebsiteDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await myFirstWebsiteDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await myFirstWebsiteDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
           return await myFirstWebsiteDbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await myFirstWebsiteDbContext.BlogPostLikes
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}

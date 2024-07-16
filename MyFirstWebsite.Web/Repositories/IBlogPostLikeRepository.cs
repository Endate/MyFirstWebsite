using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}

using Microsoft.AspNetCore.Identity;

namespace MyFirstWebsite.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}

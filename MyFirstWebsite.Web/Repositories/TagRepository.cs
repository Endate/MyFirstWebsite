using Microsoft.EntityFrameworkCore;
using MyFirstWebsite.Web.Data;
using MyFirstWebsite.Web.Models.Domain;

namespace MyFirstWebsite.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MyFirstWebsiteDbContext myFirstWebsiteDbContext;

        public TagRepository(MyFirstWebsiteDbContext myFirstWebsiteDbContext)
        {
            this.myFirstWebsiteDbContext = myFirstWebsiteDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await myFirstWebsiteDbContext.Tags.AddAsync(tag);
            await myFirstWebsiteDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await myFirstWebsiteDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                myFirstWebsiteDbContext.Tags.Remove(existingTag);
                await myFirstWebsiteDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await myFirstWebsiteDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return myFirstWebsiteDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await myFirstWebsiteDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await myFirstWebsiteDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}

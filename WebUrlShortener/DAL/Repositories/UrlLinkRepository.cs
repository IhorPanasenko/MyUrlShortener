using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class UrlLinkRepository : IUrlLinkRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<UrlLinkRepository> logger;

        public UrlLinkRepository(ApplicationDbContext dbContext, ILogger<UrlLinkRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<bool> CreateLink(UrlLink link)
        {
            try
            {
                var res = await dbContext.UrlLinks.AddAsync(link);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteLink(UrlLink link)
        {
            try
            {
                dbContext.UrlLinks.Remove(link);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<UrlLink>?> GetAllLinks()
        {
            try
            {
                return await dbContext.UrlLinks.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<UrlLink?> GetLinkInfo(Guid linkId)
        {
            try
            {
                return await dbContext.UrlLinks.FindAsync(linkId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<string?> GetLongUrl(string shortUrl)
        {
            UrlLink? urlLink = new UrlLink();
            try
            {
                urlLink = dbContext.UrlLinks.FirstOrDefault(l => l.ShortUrl == shortUrl);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }

            if (urlLink == null)
            {
                throw new ArgumentException("Given short url doesn't exist");
            }

            return urlLink.LongUrl;
        }
    }
}

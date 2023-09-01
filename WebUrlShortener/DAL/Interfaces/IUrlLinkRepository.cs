using Core.Models;

namespace DAL.Interfaces
{
    public interface IUrlLinkRepository
    {
        public Task<bool> CreateLink(UrlLink link);

        public Task<bool> DeleteLink(UrlLink link);

        public Task<UrlLink?> GetLinkInfo(Guid linkId);

        public Task<string?> GetLongUrl(string shortUrl);

        public Task<List<UrlLink>?> GetAllLinks();
    }
}

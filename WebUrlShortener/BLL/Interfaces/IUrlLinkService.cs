using Core.Models;

namespace BLL.Interfaces
{
    public interface IUrlLinkService
    {
        public Task<bool> CreateLink(UrlLink link);

        public Task<bool> DeleteLink(Guid linkId, string userId);

        public Task<UrlLink?> GetLinkInfo(Guid linkId);

        public Task<string?> GetLongUrl(string shortUrl);

        public Task<List<UrlLink>?> GetAllLinks();
    }
}

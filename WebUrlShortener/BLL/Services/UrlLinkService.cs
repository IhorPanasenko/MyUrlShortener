using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class UrlLinkService : IUrlLinkService
    {
        private readonly IUrlLinkRepository urlLinkRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly ILogger<UrlLinkService> logger;
        private int shortUrlLength = 3;

        public UrlLinkService(IUrlLinkRepository urlLinkRepository, ILogger<UrlLinkService> logger, UserManager<AppUser> userManager)
        {
            this.urlLinkRepository = urlLinkRepository;
            this.logger = logger;
            this.userManager = userManager;
        }

        public async Task<bool> CreateLink(UrlLink link)
        {
            if (!await isUserExists(link.UserId))
            {
                throw new ArgumentException("User with given Id doesn't exist");
            }

            var isUrlExists = false;

            try
            {
                isUrlExists = await isUrlExist(link.LongUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            if (isUrlExists)
            {
                throw new ArgumentException("Given Url already exists");
            }

            try
            {
                link.ShortUrl = await makeShortUrl(link.LongUrl);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            return await urlLinkRepository.CreateLink(link);
        }

        public async Task<bool> DeleteLink(Guid linkId, string userId)
        {
            UrlLink? urlLink = new UrlLink(); 

            try
            {
                urlLink = await urlLinkRepository.GetLinkInfo(linkId)??
                    throw new ArgumentException($"Can't get link with id {linkId}");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            try
            {
                var isAllowed = await isHaveAccessToDelete(urlLink, userId);

                if (isAllowed)
                {
                    return await urlLinkRepository.DeleteLink(urlLink);
                }
                else
                {
                    throw new ArgumentException("You can't delete this link");
                }
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
            
        }

        private async Task<bool> isHaveAccessToDelete(UrlLink urlLink, string userId)
        {
            var userDetails = await userManager.FindByIdAsync(userId) ?? 
                throw new ArgumentException($"No User with id {userId} was found");

            if(urlLink.UserId == userId)
            {
                return true;
            }

            var roles = await userManager.GetRolesAsync(userDetails);

            foreach (var role in roles)
            {
                if(role == "Admin")
                {
                    return true;
                }
            }

            return false; 
        }

        public async Task<List<UrlLink>?> GetAllLinks()
        {
             var links = await urlLinkRepository.GetAllLinks() ?? 
                throw new Exception("Error while trying to get links");

            foreach(UrlLink link in links)
            {
                await addUserToLink(link);
            }

            return links;
        }

        public async Task<UrlLink?> GetLinkInfo(Guid linkId)
        {
            var link =  await urlLinkRepository.GetLinkInfo(linkId) ?? 
                throw new Exception("Error while trying to get one link");

            await addUserToLink(link);
            return link;
        }

        private async Task<UrlLink> addUserToLink(UrlLink link)
        {
            var user = await userManager.FindByIdAsync(link.UserId);
            link.User = user;
            return link;
        }

        public async Task<string?> GetLongUrl(string shortUrl)
        {
            string? res;

            try
            {
                res = await urlLinkRepository.GetLongUrl(shortUrl);
            }   
            catch(ArgumentException ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

            if(res == null)
            {
                throw new Exception("Error while trying to get urlLink data");
            }

            return res;
        }

        private async Task<bool> isUserExists(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user == null ? false : true;
        }

        private async Task<bool> isUrlExist(string longUrl)
        {
            var allUrls = await urlLinkRepository.GetAllLinks() ?? throw new Exception("Error while trying to get links");

            var result = allUrls.FirstOrDefault(u => u.LongUrl == longUrl);

            if (result is not null)
            {
                return true;
            }

            return false;
        }

        private async  Task<string> makeShortUrl(string longUrl)
        {
            var hash = calculateMd5Hash(longUrl);
            var allUrls = await urlLinkRepository.GetAllLinks() ?? throw new Exception("Error while trying to get links");

            var shortUrl = "htpps://short/" + hash.Substring(0, shortUrlLength);

            while (isCacheExists(shortUrl, allUrls))
            {
                shortUrl = hash.Substring(0, ++shortUrlLength);
            }

            return shortUrl;
        }

        private string calculateMd5Hash(string longUrl)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(longUrl);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private bool isCacheExists(string shortUrl, List<UrlLink> allUrls)
        {
            return allUrls.FirstOrDefault(u => u.ShortUrl == shortUrl) != null;
        }
    }
}

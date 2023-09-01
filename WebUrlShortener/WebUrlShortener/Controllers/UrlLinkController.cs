using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebUrlShortener.ViewModels;

namespace WebUrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlLinkController : ControllerBase
    {
        private readonly IUrlLinkService urlLinkService;
        private readonly ILogger<UrlLinkController> logger;

        public UrlLinkController(IUrlLinkService urlLinkService, ILogger<UrlLinkController> logger)
        {
            this.urlLinkService = urlLinkService;
            this.logger = logger;
        }

        [HttpPost("Create")]
        [Authorize]

        public async Task<IActionResult> Create(UrlLinkViewModel urlLinkViewModel)
        {
            var url = convert(urlLinkViewModel);
            bool res = false;

            try
            {
                res = await urlLinkService.CreateLink(url);
            }
            catch(ArgumentException ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return res?Ok("Url link successfully created"):BadRequest("Failed to create Url Link");
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var links = await urlLinkService.GetAllLinks();
                return  links == null ? BadRequest("Failed to get links") : Ok(convert(links));
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLinkInfo")]
        [Authorize]
        public async Task<IActionResult> GetLinkInfo(Guid linkId)
        {
            try
            {
                var link = await urlLinkService.GetLinkInfo(linkId);
                return link == null ? BadRequest("Failed to get link") : Ok(convert(link));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        [Authorize]

        public async Task<IActionResult> DeleteUrlLink(Guid linkId, string userId)
        {
            try
            {
                var res = await urlLinkService.DeleteLink(linkId, userId);
                return res ? Ok($"link {linkId} was deleted") : BadRequest("Failed to delete a link");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("GetLongUrl")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLongUrl(string shortUrl)
        {
            try
            {
                var longUrl = await urlLinkService.GetLongUrl(shortUrl);
                return longUrl == null ? BadRequest($"Failed to get longUrl by url {shortUrl}") : Ok(longUrl);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        private List<UrlLinkViewModel> convert(List<UrlLink> links)
        {
            List<UrlLinkViewModel> list = new List<UrlLinkViewModel>();

            foreach (UrlLink link in links)
            {
                list.Add(convert(link));
            }

            return list;
        }

        private UrlLinkViewModel convert(UrlLink link)
        {
            return new UrlLinkViewModel
            {
                UrlId = link.UrlId,
                LongUrl = link.LongUrl,
                ShortUrl = link.ShortUrl,
                Description = link.Description,
                CreationDate = link.CreationDate,
                UserId = link.UserId,
                User = new AppUserViewModel
                {
                    Id = link.UserId,
                    Email = link.User!.Email,
                    UserName = link.User!.UserName,
                }
            };
        }


        private UrlLink convert(UrlLinkViewModel urlLinkViewModel)
        {
            return new UrlLink
            {
                UrlId = urlLinkViewModel.UrlId,
                UserId = urlLinkViewModel.UserId,
                Description = urlLinkViewModel.Description,
                CreationDate = urlLinkViewModel.CreationDate,
                LongUrl = urlLinkViewModel.LongUrl,
                ShortUrl = ""
            };
        } 
    }
}

using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebUrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutPageController : ControllerBase
    {
        private readonly IAboutPageService aboutPageService;
        private readonly ILogger<AboutPageController> logger;

        public AboutPageController(IAboutPageService aboutPageService, ILogger<AboutPageController> logger)
        {
            this.aboutPageService = aboutPageService;
            this.logger = logger;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePageDescription([FromBody]string description)
        {
            try
            {
                var res = await aboutPageService.Update(description);
                return res ? Ok("Description updated") : BadRequest("Failed to update description");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPageDescription")]
        public async Task<IActionResult> GetPageDescription()
        {
            try
            {
                var res = await aboutPageService.GetDescription();
                return res == null ? BadRequest("Failed to get description") : Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

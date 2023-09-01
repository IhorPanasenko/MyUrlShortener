using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AboutPageService : IAboutPageService
    {
        private readonly IAboutPageRepository aboutPageRepository;
        private readonly ILogger<AboutPageService> logger;

        public AboutPageService(IAboutPageRepository aboutPageRepository, ILogger<AboutPageService> logger)
        {
            this.aboutPageRepository = aboutPageRepository;
            this.logger = logger;
        }

        public async Task<string?> GetDescription()
        {
            try
            {
                var res = await aboutPageRepository.GetDescription() ?? throw new Exception("Failed to get description");
                return res;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> Update(string? newDescription)
        {
            if (newDescription.IsNullOrEmpty())
            {
                throw new ArgumentException("Description can't be empty");
            }

            return await aboutPageRepository.UpdatePage(newDescription!);
        }
    }
}

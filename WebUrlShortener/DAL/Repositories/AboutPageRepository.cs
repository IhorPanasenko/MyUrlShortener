using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AboutPageRepository : IAboutPageRepository
    {
        private ApplicationDbContext dbContext;
        private ILogger<AboutPageRepository> logger;

        public AboutPageRepository(ApplicationDbContext dbContext, ILogger<AboutPageRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<string?> GetDescription()
        {
            try
            {
                var res = dbContext.AboutPages.ToList()[0];
                return res.AlgorithmDescription;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdatePage(string newDescription)
        {
            try
            {
                var res = dbContext.AboutPages.ToList()[0];
                res.AlgorithmDescription = newDescription;
                dbContext.AboutPages.Update(res);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}

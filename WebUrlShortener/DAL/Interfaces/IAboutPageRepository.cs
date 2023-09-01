using Core.Models;


namespace DAL.Interfaces
{
    public interface IAboutPageRepository
    {
        public Task<bool> UpdatePage(string aboutPageModel);

        public Task<string?> GetDescription();
    }
}

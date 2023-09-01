using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAboutPageService
    {
        public Task<bool> Update(string newDescription);

        public Task<string?> GetDescription();
    }
}

using Incontra.Packer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Service
{
    public interface IUserService
    {
        bool IsLoginValid(User user);
    }
}

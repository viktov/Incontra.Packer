using Incontra.Packer.Data.Model;
using Incontra.Packer.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incontra.Packer.Data.Service
{
    public class UserService: IUserService
    {
        private IUserRepository _userReporitory;

        public UserService(IUserRepository userRepository)
        {
            _userReporitory = userRepository;
        }

        public bool IsLoginValid(User user)
        {
            return _userReporitory.IsLoginValid(user);
        }

    }
}

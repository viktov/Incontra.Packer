using Incontra.Packer.Data.Model;
using System.Collections.Generic;

namespace Incontra.Packer.Data.Repository
{
	public interface IUserRepository : IBaseRepository<User>
	{
        bool IsLoginValid(User user);
	}
}

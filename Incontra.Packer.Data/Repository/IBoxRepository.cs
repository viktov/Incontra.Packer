using Incontra.Packer.Core.Model;
using System.Collections.Generic;

namespace Incontra.Packer.Data.Repository
{
	public interface IBoxRepository : IBaseRepository<Box>
	{
        List<Box> GetByContainerID(int containerID);
	}
}

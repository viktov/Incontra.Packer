using System.Collections.Generic;

namespace Incontra.Packer.Data.Repository
{
	public interface IBaseRepository<T>
	{
		List<T> GetAll();
        T GetByID(int id);
		T Insert(T t);
		T Update(T t);
		void Delete(int id);
	}
}

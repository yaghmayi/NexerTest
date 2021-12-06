using System.Collections.Generic;
using Alpacinator.Models;

namespace Alpacinator.Business
{
    public interface IAlpacaManager
	{
		List<Alpaca> GetAll();
		Alpaca GetById(int id);
		void SaveOrUpdate(Alpaca alpaca);
		void Delete(int id);
	}
}

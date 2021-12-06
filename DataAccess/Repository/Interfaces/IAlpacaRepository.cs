using System.Collections.Generic;
using Alpacinator.Models;

namespace Alpacinator.DataAccess.Repository
{
    public interface IAlpacaRepository
    {
		List<Alpaca> GetAll();
		Alpaca GetById(int id);
		void Add(Alpaca alpaca);
		void Update(Alpaca alpaca);
		void Delete(int id);
		int GetNextId();
	}
}

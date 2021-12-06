using System.Collections.Generic;
using System.Linq;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;

namespace Alpacinator.Business
{
    public class AlpacaManager : IAlpacaManager
	{
		private readonly IAlpacaRepository _alpacaRepository;

		public AlpacaManager(IAlpacaRepository alpacaRepository)
		{
			_alpacaRepository = alpacaRepository;
		}


		public List<Alpaca> GetAll()
		{
			List<Alpaca> alcapaList = _alpacaRepository.GetAll()
													   .OrderBy(x => x.Id)
													   .ToList();

			return alcapaList;
		}

		public Alpaca GetById(int id)
		{
			Alpaca alpaca = _alpacaRepository.GetById(id);

			return alpaca;
		}

		public void SaveOrUpdate(Alpaca alpaca)
		{
            if (alpaca.Id <= 0)
            {
                alpaca.Id = _alpacaRepository.GetNextId();
				_alpacaRepository.Add(alpaca);
			}
            else
            {
                _alpacaRepository.Update(alpaca);
            }
		}

		public void Delete(int id)
		{
			_alpacaRepository.Delete(id);
		}
	}
}

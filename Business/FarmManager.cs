using System.Collections.Generic;
using Alpacinator.Business.Interfaces;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;

namespace Alpacinator.Business
{
    public class FarmManager : IFarmManager
    {
        private readonly IFarmRepository _farmRepository;

        public FarmManager(IFarmRepository farmRepository)
        {
            this._farmRepository = farmRepository;
        }

        public List<Farm> GetAll()
        {
            List<Farm> farms = _farmRepository.GetAll();

            return farms;
        }

        public Farm GetByName(string farmName)
        {
            Farm farm = _farmRepository.GetByName(farmName);

            return farm;
        }
    }
}

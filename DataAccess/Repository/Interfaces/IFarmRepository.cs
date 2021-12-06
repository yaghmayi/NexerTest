using System.Collections.Generic;
using Alpacinator.Models;

namespace Alpacinator.DataAccess.Repository
{
    public interface IFarmRepository
    {
        public List<Farm> GetAll();
        public Farm GetByName(string farmName);
    }
}

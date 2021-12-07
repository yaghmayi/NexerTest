using System.Collections.Generic;
using Alpacinator.Models;

namespace Alpacinator.Business.Interfaces
{
    public interface IFarmManager
    {
        public List<Farm> GetAll();
        public Farm GetByName(string farmName);
    }
}
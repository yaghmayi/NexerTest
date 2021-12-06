using System.Collections.Generic;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;
using Xunit;

namespace Alpacinator.UnitTests.DataAccessTests
{
    [Collection("Sequential")]
    public class FarmRepositoryTest
    {
        [Fact]
        public void GetAll()
        {
            IFarmRepository farmRepository = new FarmRepository();
            List<Farm> farms = farmRepository.GetAll();

            Assert.Collection(farms,
                              
                              item =>
                              {
                                  Assert.Equal(102, item.Id);
                                  Assert.Equal("Alpacacenter", item.Name);
                                  Assert.Equal(4, item.Multiplier);
                              },
                              item =>
                              {
                                  Assert.Equal(104, item.Id);
                                  Assert.Equal("Imported Alpacas", item.Name);
                                  Assert.Equal(7.2, item.Multiplier);
                              },
                              item =>
                              {
                                  Assert.Equal(103, item.Id);
                                  Assert.Equal("Karlssons Farm", item.Name);
                                  Assert.Equal(8.6, item.Multiplier);
                              },
                              item =>
                              {
                                  Assert.Equal(101, item.Id);
                                  Assert.Equal("Svenssons Alpacor", item.Name);
                                  Assert.Equal(1.3, item.Multiplier);
                              });
        }

        [Fact]
        public void GetByName()
        {
            IFarmRepository farmRepository = new FarmRepository();

            Farm farm = farmRepository.GetByName("Imported Alpacas");
            Assert.NotNull(farm);
            Assert.Equal(104, farm.Id);
            Assert.Equal("Imported Alpacas", farm.Name);
            Assert.Equal(7.2, farm.Multiplier);

            farm = farmRepository.GetByName("NotExistName");
            Assert.Null(farm);
        }
    }
}

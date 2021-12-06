using System.Collections.Generic;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;
using Xunit;

namespace Alpacinator.UnitTests.DataAccessTests
{
    [Collection("Sequential")]
    public class AlpacaRepositoryTest
    {
        [Fact]
        public void GetAll()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            List<Alpaca> alpacas = alpacaRepository.GetAll();

            Assert.Collection(alpacas,
                              item =>
                              {
                                  Assert.Equal(1, item.Id);
                                  Assert.Equal("Alpaca-1", item.Name);
                                  Assert.NotNull(item.Farm);
                                  Assert.Equal(101, item.Farm.Id);
                                  Assert.Equal("Svenssons Alpacor", item.Farm.Name);
                                  Assert.Equal(1.3, item.Farm.Multiplier);
                              },
                              item =>
                              {
                                  Assert.Equal(2, item.Id);
                                  Assert.Equal("Alpaca-2", item.Name);
                                  Assert.NotNull(item.Farm);
                                  Assert.Equal(103, item.Farm.Id);
                                  Assert.Equal("Karlssons Farm", item.Farm.Name);
                                  Assert.Equal(8.6, item.Farm.Multiplier);
                              });
        }
        
        [Fact]
        public void GetById()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            Alpaca alpaca = alpacaRepository.GetById(1);

            Assert.NotNull(alpaca);
            Assert.Equal(1, alpaca.Id);
            Assert.Equal("Alpaca-1", alpaca.Name);
            Assert.NotNull(alpaca.Farm);
            Assert.Equal(101, alpaca.Farm.Id);
            Assert.Equal("Svenssons Alpacor", alpaca.Farm.Name);
            Assert.Equal(1.3, alpaca.Farm.Multiplier);

            alpaca = alpacaRepository.GetById(100);
            Assert.Null(alpaca);
        }

        [Fact]
        public void Add_Update_Delete()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            IFarmRepository farmRepository = new FarmRepository();
            Alpaca alpaca = new Alpaca()
                            {
                                Id = 3,
                                Name = "Alpaca-3"
                            };
            alpaca.Farm = farmRepository.GetByName("Imported Alpacas");

            alpacaRepository.Add(alpaca);

            Alpaca createdAlpaca = alpacaRepository.GetById(3);
            Assert.NotNull(createdAlpaca);
            Assert.Equal(3, createdAlpaca.Id);
            Assert.Equal("Alpaca-3", createdAlpaca.Name);
            Assert.NotNull(alpaca.Farm);
            Assert.Equal(104, alpaca.Farm.Id);
            Assert.Equal("Imported Alpacas", alpaca.Farm.Name);
            Assert.Equal(7.2, alpaca.Farm.Multiplier);


            createdAlpaca.Name = "Alpaca-3-Edited";
            createdAlpaca.Farm = farmRepository.GetByName("Karlssons Farm");
            alpacaRepository.Update(createdAlpaca);

            Alpaca updatedAlpaca = alpacaRepository.GetById(3);
            Assert.NotNull(updatedAlpaca);
            Assert.Equal(3, updatedAlpaca.Id);
            Assert.Equal("Alpaca-3-Edited", updatedAlpaca.Name);
            Assert.Equal(103, updatedAlpaca.Farm.Id);
            Assert.Equal("Karlssons Farm", updatedAlpaca.Farm.Name);
            Assert.Equal(8.6, updatedAlpaca.Farm.Multiplier);

            alpacaRepository.Delete(3);
            alpaca = alpacaRepository.GetById(3);
            Assert.Null(alpaca);
        }

        [Fact]
        public void GetNextId()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            Assert.Equal(3, alpacaRepository.GetNextId());
        }
    }
}

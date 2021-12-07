using System.Collections.Generic;
using Alpacinator.Business;
using Alpacinator.Business.Interfaces;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;
using Xunit;

namespace Alpacinator.UnitTests.BusinessTest
{
    [Collection("Sequential")]
    public class AlpacaManagerTest
    {
        [Fact]
        public void GetAll()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            IAlpacaManager alpacaManager = new AlpacaManager(alpacaRepository);

            List<Alpaca> alpacas = alpacaManager.GetAll();

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
            IAlpacaManager alpacaManager = new AlpacaManager(alpacaRepository);

            Alpaca alpaca = alpacaManager.GetById(1);

            Assert.NotNull(alpaca);
            Assert.Equal(1, alpaca.Id); 
            Assert.Equal("Alpaca-1", alpaca.Name);
            Assert.NotNull(alpaca.Farm);
            Assert.Equal(101, alpaca.Farm.Id);
            Assert.Equal("Svenssons Alpacor", alpaca.Farm.Name);
            Assert.Equal(1.3, alpaca.Farm.Multiplier);

            alpaca = alpacaManager.GetById(100);
            Assert.Null(alpaca);
        }

        [Fact]
        public void Save_Update_Delete()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            IAlpacaManager alpacaManager = new AlpacaManager(alpacaRepository);

            IFarmRepository farmRepository = new FarmRepository();
            IFarmManager farmManager = new FarmManager(farmRepository);

            Alpaca alpaca = new Alpaca()
                            {
                                Name = "Alpaca-3"
                            };
            alpaca.Farm = farmManager.GetByName("Imported Alpacas");

            alpacaManager.SaveOrUpdate(alpaca);

            Alpaca createdAlpaca = alpacaManager.GetById(3);
            Assert.NotNull(createdAlpaca);
            Assert.Equal(3, createdAlpaca.Id);
            Assert.Equal("Alpaca-3", createdAlpaca.Name);
            Assert.NotNull(alpaca.Farm);
            Assert.Equal(104, alpaca.Farm.Id);
            Assert.Equal("Imported Alpacas", alpaca.Farm.Name);
            Assert.Equal(7.2, alpaca.Farm.Multiplier);

            createdAlpaca.Name = "Alpaca-3-Edited";
            createdAlpaca.Farm = farmManager.GetByName("Alpacacenter");
            alpacaManager.SaveOrUpdate(createdAlpaca);

            Alpaca updatedAlpaca = alpacaManager.GetById(3);
            Assert.NotNull(updatedAlpaca);
            Assert.Equal(3, updatedAlpaca.Id);
            Assert.Equal("Alpaca-3-Edited", updatedAlpaca.Name);
            Assert.NotNull(updatedAlpaca.Farm);
            Assert.Equal(102, updatedAlpaca.Farm.Id);
            Assert.Equal("Alpacacenter", updatedAlpaca.Farm.Name);
            Assert.Equal(4, updatedAlpaca.Farm.Multiplier);

            alpacaManager.Delete(3);
            alpaca = alpacaManager.GetById(3);
            Assert.Null(alpaca);
        }
    }
}
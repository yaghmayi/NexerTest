using System.Collections.Generic;
using System.Linq;
using Alpacinator.Business;
using Alpacinator.Business.Interfaces;
using Alpacinator.DataAccess.Repository;
using Alpacinator.Models;
using Alpacinator.UI.Controllers;
using Xunit;

namespace Alpacinator.UnitTests.UITests
{
    [Collection("Sequential")]
    public class AlpacaControllerTest
    {
        [Fact]
        public void GetAll()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            IAlpacaManager alpacaManager = new AlpacaManager(alpacaRepository);

            IFarmRepository farmRepository = new FarmRepository();
            IFarmManager farmManager = new FarmManager(farmRepository);

            AlpacaController alpacaController = new AlpacaController(alpacaManager, farmManager);

            List<Alpaca> alpacas = alpacaController.GetAll();

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
        public void Post()
        {
            IAlpacaRepository alpacaRepository = new AlpacaRepository();
            IAlpacaManager alpacaManager = new AlpacaManager(alpacaRepository);

            IFarmRepository farmRepository = new FarmRepository();
            IFarmManager farmManager = new FarmManager(farmRepository);

            AlpacaController alpacaController = new AlpacaController(alpacaManager, farmManager);

            Alpaca alpaca = new Alpaca()
                            {
                                Name = "Alpaca-3"
                            };
            alpaca.Farm = alpacaController.GetFarms().FirstOrDefault(x => x.Name.Equals("Svenssons Alpacor"));

            alpacaController.Post(alpaca);

            Alpaca createdAlpaca = alpacaController.GetById(3);
            Assert.NotNull(createdAlpaca);
            Assert.Equal(3, createdAlpaca.Id);
            Assert.Equal("Alpaca-3", createdAlpaca.Name);
            Assert.NotNull(createdAlpaca.Farm);
            Assert.Equal(101, createdAlpaca.Farm.Id);
            Assert.Equal("Svenssons Alpacor", createdAlpaca.Farm.Name);
            Assert.Equal(1.3, createdAlpaca.Farm.Multiplier);

            createdAlpaca.Name = "Alpaca-3-Edited";
            createdAlpaca.Farm = alpacaController.GetFarms().FirstOrDefault(x => x.Name.Equals("Karlssons Farm"));
            alpacaController.Post(createdAlpaca);

            Alpaca updatedAlpaca = alpacaController.GetById(3);
            Assert.NotNull(updatedAlpaca);
            Assert.Equal(3, updatedAlpaca.Id);
            Assert.Equal("Alpaca-3-Edited", updatedAlpaca.Name);
            Assert.NotNull(updatedAlpaca.Farm);
            Assert.Equal(103, updatedAlpaca.Farm.Id);
            Assert.Equal("Karlssons Farm", updatedAlpaca.Farm.Name);
            Assert.Equal(8.6, updatedAlpaca.Farm.Multiplier);

            alpacaController.Delete(3);
            alpaca = alpacaController.GetById(3);
            Assert.Null(alpaca);
        }
    }
}
using System.Collections.Generic;
using Alpacinator.Business;
using Alpacinator.Business.Interfaces;
using Alpacinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alpacinator.UI.Controllers
{
	[Route("nexerTest/api/alpaca")]
	public class AlpacaController : Controller
	{
		
		private readonly IAlpacaManager _alpacaManager;
		private readonly IFarmManager _farmManager;

		public AlpacaController(IAlpacaManager alpacaManager, IFarmManager farmManager)
		{
			this._alpacaManager = alpacaManager;
            this._farmManager = farmManager;
        }

		[HttpGet]
		[Route("list")]
		public List<Alpaca> GetAll()
		{
			List<Alpaca> alpacaList = _alpacaManager.GetAll();

			return alpacaList;
		}

		[HttpGet]
		[Route("getById/{id:int}")]
		public Alpaca GetById(int id)
		{
			Alpaca alpaca = _alpacaManager.GetById(id);

			return alpaca;
		}

		[HttpPost]
		[Route("add")]
		public IActionResult Post([FromBody] Alpaca alpaca)
		{
			_alpacaManager.SaveOrUpdate(alpaca);

			return Ok();
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public IActionResult Delete(int id)
		{
			_alpacaManager.Delete(id);

			return Ok();
		}

        [HttpGet]
        [Route("farms")]
        public List<Farm> GetFarms()
        {
            List<Farm> farms = _farmManager.GetAll();

            return farms;
        }
	}
}

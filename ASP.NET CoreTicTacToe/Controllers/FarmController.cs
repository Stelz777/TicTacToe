using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_CoreTicTacToe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]

   


    public class FarmController : ControllerBase
    {
        private GameFarm farm;

        public FarmController(GameFarm farm)
        {
            this.farm = farm;
        }

        [HttpGet]
        public IActionResult GetGame(int? id)
        {
            var (gameId, game) = farm.FindGame(id);
            var history = game.History;
            return Ok(new 
            {
                id = gameId, 
                history
            });
        }
    }
}
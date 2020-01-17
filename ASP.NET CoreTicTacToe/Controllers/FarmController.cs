using System.Collections.Generic;
using ASP.NET_CoreTicTacToe.Models;
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
            List<Board> boards = history.GetBoardsForEachTurn();
            
            return Ok(new 
            {
                id = gameId, 
                boards
            });
        }
    }
}
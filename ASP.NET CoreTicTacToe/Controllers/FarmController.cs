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
        private TicTacToeContext database;

        public FarmController(GameFarm farm, TicTacToeContext database)
        {
            this.farm = farm;
            this.database = database;
        }

        [HttpGet]
        public IActionResult GetGame(int? id)
        {
            var (gameId, game) = farm.GetGame(id, database);
            var history = game.History;
            List<Board> boards = history.GetBoardsForEachTurn();
            database.SaveChanges();
            return Ok(new 
            {
                id = gameId, 
                boards
            });
        }
    }
}
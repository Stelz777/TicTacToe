using System.Collections.Generic;
using ASP.NET_CoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private GameFarm farm;
        private TicTacToeContext database;
        private IMapper mapper;

        public FarmController(GameFarm farm, TicTacToeContext database, IMapper mapper)
        {
            this.farm = farm;
            this.database = database;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGame(int? id)
        {
            var databaseWorker = new DatabaseWorker(database, mapper);
            var (gameId, game) = farm.GetGame(id, databaseWorker);
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
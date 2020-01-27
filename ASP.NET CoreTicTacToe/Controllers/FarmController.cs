using System.Collections.Generic;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreTicTacToe.Controllers
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
            var gameAPI = new GameAPI(database, mapper);
            var (gameId, game) = farm.GetGame(id, gameAPI);
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
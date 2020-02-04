using System.Collections.Generic;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private GameAPI gameAPI;

        public FarmController(GameAPI gameAPI)
        {
            this.gameAPI = gameAPI;
        }

        [HttpGet("{id?}")]
        public IActionResult GetGame(int? id, string bot)
        {
            
            var (gameId, game) = gameAPI.GetGame(id, bot);
            var history = game.History;
            var boards = history.GetBoardsForEachTurn();
            
            return Ok(new 
            {
                id = gameId, 
                boards
            });
        }
    }
}
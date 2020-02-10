using System.Collections.Generic;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private GameAPI gameAPI;

        public LobbyController(GameAPI gameAPI)
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
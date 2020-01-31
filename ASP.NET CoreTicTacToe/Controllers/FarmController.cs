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
        private GameAPI gameAPI;

        public FarmController(GameAPI gameAPI)
        {
            this.gameAPI = gameAPI;
        }

        [HttpGet]
        public IActionResult GetGame(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id);
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
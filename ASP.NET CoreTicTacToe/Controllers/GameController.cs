using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ASP.NETCoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class GameController : ControllerBase
    { 
        private GameAPI gameAPI;
        
        public GameController(GameAPI gameAPI)
        {
            this.gameAPI = gameAPI;
        }

        [HttpPost]
        public void UpdateGame(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id, null);
            gameAPI.UpdateGame(game, gameId);
        }

        [HttpPost]
        public void MakeBotMove(int? id, string player)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            var opponent = game.GetOpponent(player);
            if (opponent != null)
            {
                if (opponent.Bot != null)
                {
                    var bot = opponent.Bot;
                    bot.MakeAutoMove();
                }
            }
        }

        [HttpGet]
        public IActionResult Updates(int? id, string playerName)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            var lastTurn = game.History.LastTurn;
            if (lastTurn.CellNumber == -1)
            {
                return NotFound();
            }
            var requesterSide = game.GetSideByName(playerName);
            if (lastTurn.Side != requesterSide || playerName == null)
            {
                return Ok(new
                {
                    lastTurn
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            bool result = game.MakeMove(turn);
            return result;
        }

        [HttpPost]
        public Side SetName(int? id, Player player)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            if (player != null)
            {
                var side = game.SetName(player.Name);
                return side;
            }
            else
            {
                throw new ArgumentException("No player specified by client.");
            }
        }

        
    }
}
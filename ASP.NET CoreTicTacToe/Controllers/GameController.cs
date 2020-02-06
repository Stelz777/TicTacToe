using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        

        [HttpGet]
        public IActionResult Updates(int? id, int currentTurn)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            var resultTurns = game.History.Turns.Skip(currentTurn).ToList();
            return Ok(resultTurns);
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            bool result = game.MakeMove(turn);
            UpdateGame(id);
            MakeBotMove(id, game.TicPlayer.Side == turn.Side 
                ? game.TicPlayer.Name 
                : game.TacPlayer.Side == turn.Side
                ? game.TacPlayer.Name
                : null);
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

        private void MakeBotMove(int? id, string player)
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

        private void UpdateGame(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id, null);
            gameAPI.UpdateGame(game, gameId);
        }
    }
}
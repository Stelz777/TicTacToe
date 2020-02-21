using ASP.NETCoreTicTacToe.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var ticPlayerName = game.TicPlayer.Name;
            var tacPlayerName = game.TacPlayer.Name;
            if (game.TicPlayer.IsBot 
             && game.TacPlayer.IsBot 
             && !game.Board.HasWinner 
             && game.Board.Squares.Contains(Cell.Empty))
            {
                var botManager = new BotManager();
                botManager.PlayBotVsBot(game);
                gameAPI.UpdateGame(game, id.Value);
            }
            var continueUpdating = true;
            var resultTurns = game.History.Turns.Skip(currentTurn).ToList();
            if (game.TicPlayer.IsBot
             && game.TacPlayer.IsBot
             && (game.Board.HasWinner
             || !game.Board.Squares.Contains(Cell.Empty)))
            {
                continueUpdating = false;
            }
            return Ok(new
            {
                Turns = resultTurns,
                ticPlayerName,
                tacPlayerName,
                continueUpdating
            });
        }

        [HttpPost]
        public bool MakeTurn(int? id, string name, Turn turn)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            bool result = game.MakeMove(turn);
            gameAPI.UpdateGame(game, id.Value);
            MakeBotMove(id, name, game);
            gameAPI.UpdateGame(game, id.Value);
            return result;
        }

        private void MakeBotMove(int? id, string player, Game game)
        {
            game.MakeBotMove(player);
        }

        [HttpPost]
        public IActionResult SetSide(int? id, string name)
        {
            var (_, game) = gameAPI.GetGame(id, null);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var side = game.SetSide(name);
            gameAPI.UpdateGame(game, id.Value);
            if (side != null)
            {
                return Ok(side);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
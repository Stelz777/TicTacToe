using ASP.NET_CoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ASP.NET_CoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (_, game) = GameFarm.Current.FindGame(id);
            var newBoard = new Board();
            newBoard.SetSquares(game.Board.Squares);
            var bot = new SimpleBot(game);
            BotFarm.Current.AddBotToGroup(bot);
            var turn = bot.MakeAutoMove();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = GameFarm.Current.FindGame(id);
            return game.MakeMove(turn);
        }
    }
}
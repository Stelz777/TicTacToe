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
        private readonly GameFarm farm;

        public GameController(GameFarm farm, ILogger<GameController> logger)
        {
            _logger = logger;
            this.farm = farm;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (_, game) = farm.FindGame(id);
            var newBoard = new Board();
            newBoard.SetSquares(game.Board.Squares);
            var bot = new SimpleBot(game);
            var botFarm = new BotFarm();
            botFarm.AddBotToGroup(bot);
            var turn = bot.MakeAutoMove();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = farm.FindGame(id);
            return game.MakeMove(turn);
        }
    }
}
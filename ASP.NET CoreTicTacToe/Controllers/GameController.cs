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
        private BotFarm botFarm;
        private GameFarm gameFarm;

        public GameController(BotFarm botFarm, GameFarm gameFarm, ILogger<GameController> logger)
        {
            _logger = logger;
            this.botFarm = botFarm;
            this.gameFarm = gameFarm;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (_, game) = gameFarm.FindGame(id);
            var bot = new SimpleBot(game);
            bot.InitSide("Tac");
            botFarm.AddBotToPool(bot);
            var turn = bot.MakeAutoMove();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = gameFarm.FindGame(id);
            return game.MakeMove(turn);
        }
    }
}
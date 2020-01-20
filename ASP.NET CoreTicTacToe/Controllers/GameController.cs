using ASP.NET_CoreTicTacToe.Models;
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
        TicTacToeContext database;

        public GameController(BotFarm botFarm, GameFarm gameFarm, TicTacToeContext context, ILogger<GameController> logger)
        {
            _logger = logger;
            this.botFarm = botFarm;
            this.gameFarm = gameFarm;
            database = context;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (_, game) = gameFarm.GetGame(id, database);
            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);
            var turn = bot.MakeAutoMove();
            database.SaveChanges();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            (_, var game) = gameFarm.GetGame(id, database);
            bool result = game.MakeMove(turn);
            database.SaveChanges();
            return result;
        }
    }
}
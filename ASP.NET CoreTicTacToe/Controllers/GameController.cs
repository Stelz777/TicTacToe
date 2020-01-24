using ASP.NET_CoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ASP.NETCoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<GameController> _logger;
        private BotFarm botFarm;
        private GameFarm gameFarm;
        TicTacToeContext database;

        public GameController(BotFarm botFarm, GameFarm gameFarm, TicTacToeContext context, ILogger<GameController> logger, IMapper mapper)
        {
            _logger = logger;
            this.botFarm = botFarm;
            this.gameFarm = gameFarm;
            database = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var gameAPI = new GameAPI(database, mapper);
            var (_, game) = gameFarm.GetGame(id, gameAPI);
            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);
            var turn = bot.MakeAutoMove(gameAPI);
            database.SaveChanges();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var gameAPI = new GameAPI(database, mapper);
            (_, var game) = gameFarm.GetGame(id, gameAPI);
            bool result = game.MakeMove(turn, gameAPI);
            database.SaveChanges();
            return result;
        }
    }
}
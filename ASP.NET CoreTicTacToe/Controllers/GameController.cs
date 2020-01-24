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
            var databaseWorker = new DatabaseWorker(database, mapper);
            var (_, game) = gameFarm.GetGame(id, databaseWorker);
            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);
            var turn = bot.MakeAutoMove(databaseWorker);
            database.SaveChanges();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var databaseWorker = new DatabaseWorker(database, mapper);
            (_, var game) = gameFarm.GetGame(id, databaseWorker);
            bool result = game.MakeMove(turn, databaseWorker);
            database.SaveChanges();
            return result;
        }
    }
}
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
        private GameAPI gameAPI;
        
        TicTacToeContext database;

        public GameController(GameAPI gameAPI, BotFarm botFarm, TicTacToeContext context, ILogger<GameController> logger, IMapper mapper)
        {
            _logger = logger;
            this.botFarm = botFarm;
            
            database = context;
            this.mapper = mapper;
            this.gameAPI = gameAPI;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id);
            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);
            var turn = bot.MakeAutoMove();
            var gameWithAPI = new GameWithAPI(game, gameAPI);
            SaveContext(gameWithAPI, gameId);
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, game) = gameAPI.GetGame(id);
            bool result = game.MakeMove(turn);
            return result;
        }


        public static void SaveContext(GameWithAPI gameWithAPI, int gameId)
        {
            if (gameWithAPI != null)
            {
                if (!gameWithAPI.Game.CanContinue())
                {
                    gameWithAPI.GameAPI.UpdateGame(gameWithAPI.Game, gameId);
                }
            }
        }
    }
}
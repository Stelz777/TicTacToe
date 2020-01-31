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
        public Turn BotTurn(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id);

            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);

            var turn = bot.MakeAutoMove();
            UpdateGame(game, gameId);

            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (gameId, game) = gameAPI.GetGame(id);
            var turn = game.History.Turns[game.History.Turns.Count - 1];
            UpdateGame(game, gameId);
            _logger.LogInformation($"Turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public IActionResult Updates(int? id, RealPlayer player)
        {
            var (gameId, game) = gameAPI.GetGame(id);
            var lastTurn = game.History.Turns[game.History.Turns.Count - 1];
            if (lastTurn.CellNumber == -1)
            {
                return NotFound();
            }
            Side requesterSide;
            if (game.TicPlayer.Name == player.Name)
            {
                requesterSide = game.TicPlayer.Side;
            }
            else if (game.TacPlayer.Name == player.Name)
            {
                requesterSide = game.TacPlayer.Side;
            }
            else
            {
                return NotFound();
            }
            if (lastTurn.WhichTurn != requesterSide)
            {
                UpdateGame(game, gameId);
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
            var (_, game) = gameAPI.GetGame(id);
            bool result = game.MakeMove(turn);
            return result;
        }

        [HttpPost]
        public Side SetName(int? id, RealPlayer player)
        {
            var (_, game) = gameAPI.GetGame(id);
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

        private void UpdateGame(Game game, int gameId)
        {
             gameAPI.UpdateGame(game, gameId);
        }
    }
}
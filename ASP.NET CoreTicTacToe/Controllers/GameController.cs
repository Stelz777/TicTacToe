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
        public IActionResult Updates(int? id, Player player)
        {
            var (gameId, game) = gameAPI.GetGame(id);
            UpdateGame(game, gameId);
            var lastTurn = game.History.Turns[game.History.Turns.Count - 1];
            if (lastTurn.CellNumber == -1)
            {
                return NotFound();
            }
            Side requesterSide;
            Player opponent;
            if (game.TicPlayer.Name == player.Name)
            {
                requesterSide = game.TicPlayer.Side;
                opponent = game.TacPlayer;
            }
            else if (game.TacPlayer.Name == player.Name)
            {
                requesterSide = game.TacPlayer.Side;
                opponent = game.TicPlayer;
            }
            else
            {
                return NotFound();
            }
            if (opponent.IsBot)
            {
                try
                {
                    var bot = (IBot)opponent;
                    lastTurn = bot.MakeAutoMove();
                    return Ok(new
                    {
                        lastTurn
                    });
                }
                catch (Exception exception)
                {
                    return NotFound();
                }
            }
            if (lastTurn.WhichTurn != requesterSide)
            {
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
        public Side SetName(int? id, Player player)
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
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
        private readonly Farm farm;

        public GameController(Farm farm, ILogger<GameController> logger)
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
            var turn = game.Bot.MakeAutoMove(newBoard);
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
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
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly Farm farm;

        public BoardController(Farm farm, ILogger<BoardController> logger)
        {
            _logger = logger;
            this.farm = farm;
        }

        [HttpPost]
        public Turn NextTurn(int? id)
        {
            var (_, game) = farm.FindGame(id);
            var turn = game.MakeAutoMove();
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
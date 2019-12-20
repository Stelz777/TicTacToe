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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private Board board;

        private readonly ILogger<BoardController> _logger;

        public BoardController(Farm farm, ILogger<BoardController> logger)
        {
            board = farm.boards[farm.currentGame];
            _logger = logger;
        }

        [HttpPost]
        public Turn NextTurn()
        {
            var random = new Random();
            int randomTurn = random.Next(1, 9);
            _logger.LogInformation($"Bot turn: { randomTurn }");
            return new Turn
            {
                CellNumber = randomTurn
            };
        }

        [HttpPost]
        public void SetBoard(Turn turn)
        {
            if (turn.TicTurn)
            {
                board.Squares[turn.CellNumber] = Cell.Cross;
            }
            else
            {
                board.Squares[turn.CellNumber] = Cell.Nought;
            }
            
        }

        [HttpPost]
        public Board GetBoard()
        {
            return board;
        }
    }
}
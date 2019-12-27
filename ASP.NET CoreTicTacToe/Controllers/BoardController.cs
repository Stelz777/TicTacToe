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
            //return null;
            var (_, history) = farm.FindHistory(id);
            Board newBoard = new Board();
            newBoard.SetSquares(history.Turns[history.Turns.Count - 1].Squares);
            Turn turn = newBoard.MakeAutoMove();
            if (turn.CellNumber != -1)
            {
                history.Turns.Add(newBoard);
            }
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        [HttpPost]
        public bool MakeTurn(int? id, Turn turn)
        {
            var (_, history) = farm.FindHistory(id);
            Board newBoard = new Board();
            return newBoard.SetBoard(history, turn);
            
        }


    }
}
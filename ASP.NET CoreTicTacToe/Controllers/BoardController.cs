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
    [Route("/api/[controller]/[action]")]
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

        //[HttpPost]
        public Turn NextTurn(int boardId)
        {
            var (_, turn) = farm.FindBoard(boardId).MakeAutoMove();
            _logger.LogInformation($"Bot turn: {turn.CellNumber}");
            return turn;
        }

        // [HttpPost]
        // public void SetBoard(Turn turn)
        // {
        //     if (!board.HasWinner(board))
        //     {
        //         if (board.Squares[turn.CellNumber] == Cell.Empty)
        //         {

        //             board.Squares[turn.CellNumber] = Cell.Cross;

        //             if (!board.HasWinner(board))
        //             {
        //                 Turn nextTurn = NextTurn();
        //                 if (nextTurn.CellNumber >= 0)
        //                 {
        //                     board.Squares[nextTurn.CellNumber] = Cell.Nought;
        //                 }
        //             }
        //         }
        //     }
        // }

        // [HttpPost]
        // public Board GetBoard()
        // {
        //     return board;
        // }
    }
}